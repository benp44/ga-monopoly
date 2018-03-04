using System.Collections.Generic;
using System.Drawing;

namespace MG
{
    public abstract class Asset
    {
        public string Name { get; private set; }
        public int Order { get; private set; }
        public abstract Color AssetColor { get; }
        public int Price { get; private set; }
        public List<Asset> MonopolySet { get; private set; }
        public IAssetHolder Owner { get; set; }
        public bool Mortgaged { get; private set; }
        public int MortgageValue { get; private set; }

        public Asset(string name, int order, int price, int mortgageValue)
        {
            Name = name;
            MonopolySet = new List<Asset>();
            Owner = null;
            Order = order;
            Price = price;
            MortgageValue = mortgageValue;
        }

        public int MortgageAsset()
        {
            Mortgaged = true;
            return MortgageValue;
        }

        public int UnmortgageAsset()
        {
            Mortgaged = false;

            return (int)(MortgageValue * 1.1);
        }

        public bool OwnerHasMonopoly
        {
            get
            {
                return MonopolySet.TrueForAll(m => m.Owner == this.Owner);
            }
        }

        public abstract int CalculateRent(int diceRoll);

        public override string ToString()
        {
            return Name;
        }
    }
}
