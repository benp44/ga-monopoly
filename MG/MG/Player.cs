using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MG
{
    public class Player : IAssetHolder
    {
        public Guid ID { get; private set; }
        public int Money { get; private set; }
        public Square Location { get; set; }
        public PlayerStatus Status { get; private set; }
        public List<Asset> Assets { get; private set; }
        public Queue<Card> GetOutOfJailFreeCards { get; private set; }
        public List<string> LastTurnEvents { get; private set; }
        public int TurnsInJail { get; set; }
        public GeneticCode Genetics { get; private set; }

        public IEnumerable<Property> Properties
        {
            get
            {
                return new List<Property>(Assets.Where(x => x is Property).Cast<Property>());
            }
        }

        public IEnumerable<Property> PropertiesWithMonopoly
        {
            get
            {
                return new List<Property>(Assets.Where(x => ((x is Property) && x.MonopolySet.TrueForAll(y => y.Owner == this))).Cast<Property>());
            }
        }

        public Player() : this (new GeneticCode())
        {
        }

        public Player(GeneticCode genetics)
        {
            Assets = new List<Asset>();
            Genetics = genetics;
            GetOutOfJailFreeCards = new Queue<Card>();
            TurnsInJail = 0;
            Money = (2 * 500) + (2 * 100) + (2 * 50) + (6 * 20) + (5 * 10) + (5 * 5) + (5 * 1);
            Status = PlayerStatus.Normal;
            LastTurnEvents = new List<string>();
            ID = Guid.NewGuid();
        }

        public void Pay(int amount, IAssetHolder payee, Bank bank)
        {
            LogEvent("Pay", amount);

            if (amount < 0)
            {
                throw new ApplicationException("Can't pay less than 0");
            }

            if ((Money - amount) < 0)
            {
                // can't afford it 

                var sumReached = false;

                while (sumReached == false)
                {
                    var assetToSellBuilding = Properties.Where(b => b.BuildingCount > 0)
                                                        .Where(c => c.MonopolySet.Max(m => (((Property)m).BuildingCount)) == c.BuildingCount)
                                                        .OrderBy(o => Genetics.GetGeneExpression(GeneType.ImprovementValue, o))
                                                        .FirstOrDefault();

                    if (assetToSellBuilding != null)
                    {
                        Money += assetToSellBuilding.SellBuilding();

                        if ((Money - amount) >= 0)
                        {
                            sumReached = true;
                        }
                    }
                    else // no buildings, start mortgaging
                    {
                        var assetForMortaging = Assets.Where(x => x.Mortgaged == false)
                                                      .OrderBy(o => Genetics.GetGeneExpression(GeneType.PropertyValue, o))
                                                      .FirstOrDefault();

                        if (assetForMortaging != null)
                        {
                            Money += assetForMortaging.MortgageAsset();

                            if ((Money - amount) >= 0)
                            {
                                sumReached = true;
                            }
                        }
                        else
                        {
                            // Nothing more to mortgage, broke!
                            break;
                        }
                    }
                }

                if (sumReached == false)
                {
                    // Couldn't pay. Hand over all assets to debtor

                    if (payee is FreeParking || payee is Bank)
                    {
                        foreach (var asset in Assets)
                        {
                            asset.Owner = bank;
                            bank.Assets.Add(asset);
                            asset.UnmortgageAsset();
                        }
                    }
                    else
                    {
                        foreach (var asset in Assets)
                        {
                            asset.Owner = payee;
                            payee.Assets.Add(asset);
                        }
                    }

                    Assets.Clear();

                    // Lose get out of jail free cards

                    while (GetOutOfJailFreeCards.Count > 0)
                    {
                        GetOutOfJailFreeCards.Dequeue();
                    }

                    // All money turned over

                    payee.Receive(Money);
                    Money = 0;

                    Status = PlayerStatus.Bankrupt;
                    LogEvent("Bankrupt trying to pay ", amount);

                    return;
                }
            }

            Money -= amount;
            payee.Receive(amount);
        }

        public void Receive(int amount)
        {
            LogEvent("Receive", amount);

            if (amount < 0)
            {
                throw new ApplicationException("Can't receive less than 0");
            }

            Money += amount;
        }

        public void SetCashTotal(int total)
        {
            Money = total;
        }

        public void FreeFromJail()
        {
            LogEvent("Freed from jail");

            TurnsInJail = 0;
            Status = PlayerStatus.Normal;
        }

        public void SetInJail()
        {
            LogEvent("Sent to Jail");

            Status = PlayerStatus.InJail;
            TurnsInJail = 0;
        }

        public void ConsiderPurchase(Asset asset)
        {
            if (Money > asset.Price)
            {
                var value = Genetics.GetGeneExpression(GeneType.PropertyValue, asset);
                if ((value * Money) > asset.Price)
                {
                    PurchaseAsset(asset);
                }
            }
        }

        public void PurchaseAsset(Asset asset)
        {
            LogEvent("Purchse", asset.Name);

            if (Money < asset.Price)
            {
                throw new ApplicationException("Player could not afford " + asset.Name);
            }

            if ((asset.Owner is Bank) == false)
            {
                if (asset.Owner == this)
                {
                    throw new ApplicationException("Player tried to purchase, but they already own " + asset.Name);
                }
                else
                {
                    throw new ApplicationException("Player tried to purchase, but someone else owns " + asset.Name);
                }
            }

            asset.Owner.Assets.Remove(asset);
            asset.Owner = this;
            Assets.Add(asset);
            Money -= asset.Price;
        }

        public void ConsiderBuilding()
        {
            bool bContinue = true;

            while (bContinue)
            {
                var assetToBuildOn = Properties.Where(p => p.OwnerHasMonopoly &&
                                                      p.BuildingCost <= Money &&
                                                      p.BuildingCount < 5 &&
                                                      p.BuildingPossible).OrderByDescending(o => Genetics.GetGeneExpression(GeneType.ImprovementValue, o)).FirstOrDefault();

                if (assetToBuildOn != null)
                {
                    PurchaseBuilding(assetToBuildOn);
                }
                else
                {
                    bContinue = false;
                }
            }
        }

        public void PurchaseBuilding(Property property)
        {
            LogEvent("Improve", property.Name);

            if (property.Owner != this)
            {
                throw new ApplicationException("Attempted to buy a house, but didn't own the property");
            }

            if (property.MonopolySet.TrueForAll(x => x.Owner == this) == false)
            {
                throw new ApplicationException("Attempted to buy a house, but didn't have monopoly");
            }

            if (property.BuildingCost > Money)
            {
                throw new ApplicationException("Attempted to buy a house, but couldn't afford it");
            }

            if (property.BuildingCount == 5)
            {
                throw new ApplicationException("Attempted to buy a house, but hotel already exists");
            }

            foreach (var otherPropertyInSet in property.MonopolySet.Excluding(property))
            {
                if (property.BuildingCount > ((Property)otherPropertyInSet).BuildingCount)
                {
                    throw new ApplicationException("Attempted to build houses unevenly");
                }
            }

            Money -= property.Improve();
        }

        public void SellBuilding(Property property)
        {
            LogEvent("Sell building", property.Name);

            if (property.BuildingCount == 0)
            {
                throw new ApplicationException("No buildings to sell");
            }

            if (property.MonopolySet.Excluding(property).ToList().TrueForAll(x => ((Property)x).BuildingCount == property.BuildingCount || ((Property)x).BuildingCount == (property.BuildingCount - 1)) == false)
            {
                throw new ApplicationException("tried to sell building unevenly");
            }

            Money += property.SellBuilding();
        }

        private int MortgageAsset(Asset asset)
        {
            LogEvent("Mortgage", asset.Name);

            return asset.MortgageAsset();
        }

        public void ConsiderUnmortgaging()
        {
            while (true)
            {
                var assetToUnmortgage = Assets.Where(a => a.Mortgaged && (a.MortgageValue * 1.1) < Money).OrderByDescending(o => Genetics.GetGeneExpression(GeneType.PropertyValue, o)).FirstOrDefault();
                if (assetToUnmortgage != null)
                {
                    UnmortgageAsset(assetToUnmortgage);
                }
                else
                {
                    break;
                }
            }
        }

        private void UnmortgageAsset(Asset assetToUnmortgage)
        {
            LogEvent("Unmortgage", assetToUnmortgage.Name);

            Money -= assetToUnmortgage.UnmortgageAsset();
        }

        public void LogEvent(params object[] values)
        {
            var str = new StringBuilder();

            foreach (var value in values)
            {
                str.Append(value);
                if (value != values[values.Length - 1])
                {
                    str.Append(" ");
                }
            }

            LastTurnEvents.Add(str.ToString());
        }

        internal void Trade(List<Player> players)
        {
            var possibleExchanges = new List<Tuple<Asset, Asset, double>>();

            foreach (var myAsset in Assets.Where(a => ((a is Property) && ((Property)a).BuildingCount == 0) || ((a is Property) == false)))
            {
                foreach (var otherPlayer in players.Excluding(this))
                {
                    foreach (var theirAsset in otherPlayer.Assets.Where(a => ((a is Property) && ((Property)a).BuildingCount == 0) || ((a is Property) == false)))
                    {
                        var myValueOfMine = Genetics.GetGeneExpression(GeneType.PropertyValue, myAsset) * myAsset.Price;
                        var myValueOfTheirs = Genetics.GetGeneExpression(GeneType.PropertyValue, theirAsset) * theirAsset.Price;

                        var theirValueOfMine = otherPlayer.Genetics.GetGeneExpression(GeneType.PropertyValue, myAsset) * myAsset.Price;
                        var theirValueOfTheirs = otherPlayer.Genetics.GetGeneExpression(GeneType.PropertyValue, theirAsset) * theirAsset.Price;

                        // Double the value for monopolies

                        if (myAsset.OwnerHasMonopoly)
                        {
                            myValueOfMine *= 2;
                        }

                        if (theirAsset.OwnerHasMonopoly)
                        {
                            theirValueOfTheirs *= 2;
                        }

                        if (theirAsset.MonopolySet.Excluding(theirAsset).ToList().TrueForAll(o => o.Owner == this))
                        {
                            myValueOfTheirs *= 2;
                        }

                        if (myAsset.MonopolySet.Excluding(myAsset).ToList().TrueForAll(o => o.Owner == otherPlayer))
                        {
                            theirValueOfMine *= 2;
                        }

                        if (theirValueOfMine > myValueOfMine && theirValueOfTheirs < myValueOfTheirs)
                        {
                            // Don't swap if i'm not net gaining net value (although the swap will likely happen on their turn)

                            if ((myValueOfTheirs - myValueOfMine) > 0)
                            {
                                possibleExchanges.Add(new Tuple<Asset, Asset, double>(myAsset, theirAsset, myValueOfTheirs - myValueOfMine));
                            }
                        }
                    }
                }
            }

            // Select the best if multiple exchanges exist

            var bestExchanges = new List<Tuple<Asset, Asset>>();

            //foreach (var myAsset in possibleExchanges.Select(x => x.Item1).Distinct())
            {
                var bestSwap = possibleExchanges.OrderByDescending(o => o.Item3).FirstOrDefault();
                if (bestSwap != null)
                {
                    //bestExchanges.Add(new Tuple<Asset, Asset>(bestSwap.Item1, bestSwap.Item2));

                    var otherPlayer = bestSwap.Item2.Owner;
                    var myAsset = bestSwap.Item1;
                    var theirAsset = bestSwap.Item2;

                    LogEvent("Swapped " + myAsset.Name + " for " + theirAsset.Name);

                    this.Assets.Remove(myAsset);
                    otherPlayer.Assets.Add(myAsset);
                    myAsset.Owner = otherPlayer;
                    otherPlayer.Assets.Remove(theirAsset);
                    this.Assets.Add(theirAsset);
                    theirAsset.Owner = this;
                }
            }
        }
    }
}
