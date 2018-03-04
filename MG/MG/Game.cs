using System;
using System.Collections.Generic;
using System.Linq;

namespace MG
{
    public class Game
    {
        public List<Player> Players { get; private set; }
        public ICardManager CardManager { get; private set; }
        public IDice Dice { get; private set; }
        public Player NextPlayer { get; private set; }
        public Board Board { get; private set; }
        public AssetManager AssetManager { get; private set; }
        public int PlayerTurnsPlayed { get; private set; }
        public Bank Bank { get; private set; }
        public FreeParking FreeParking { get; private set; }
        public Player Winner { get; private set; }

        public Game(List<Player> players)
        {
            var bank = new Bank();
            var board = new Board(new AssetManager());
            var dice = new Dice();
            var cardManager = new CardManager(board);

            Initialise(board, bank, dice, cardManager, players);
        }

        public Game(Board board, Bank bank, IDice dice, ICardManager cardManager, List<Player> players)
        {
            Initialise(board, bank, dice, cardManager, players);
        }

        private void Initialise(Board board, Bank bank, IDice dice, ICardManager cardManager, List<Player> players)
        {
            Bank = bank;
            Board = board;
            AssetManager = board.AssetManager;
            Dice = dice;
            Players = players;
            CardManager = cardManager;
            Winner = null;
            PlayerTurnsPlayed = 0;

            FreeParking = new FreeParking(Bank);

            // Set players on Go

            foreach (var player in players)
            {
                player.Location = Board.Go;
            }

            // Bank owns everything to start

            board.AssetManager.AllAssets.ForEach(x => bank.PurchaseAsset(x));

            NextPlayer = players[0];
        }

        public Player PlayToEnd()
        {
            while (PlayTurn() == false)
            {
            }

            return Winner;
        }

        public bool PlayTurn()
        {
            do
            {
                NextPlayer.LastTurnEvents.Clear();
                if (PlayPlayerTurn(NextPlayer))
                {
                    return true;
                }
            }
            while (NextPlayer != Players.First() && Winner == null);

            return false;
        }

        public bool PlayPlayerTurn(Player player)
        {
            if (Winner != null )
            {
                return true;
            }

            PlayerTurnsPlayed++;

            var bContinueMoving = true;
            var doubleCount = 0;

            while (bContinueMoving == true && player.Status != PlayerStatus.Bankrupt)
            {
                var diceResult = Dice.RollDice();

                player.LogEvent("Rolled " + diceResult.ToString());

                if (player.Status == PlayerStatus.InJail)
                {
                    // Players in jail don't get to move this turn,
                    // but there a several ways to get out

                    bContinueMoving = false;

                    if (diceResult.DoubleRolled)
                    {
                        // A double - free but no second roll this time.

                        player.FreeFromJail();
                    }
                    else if (player.GetOutOfJailFreeCards.Count > 0)
                    {
                        // GOOJF
                        player.FreeFromJail();

                        CardManager.ReturnGetOutOfJailFreeCard(player.GetOutOfJailFreeCards.Dequeue());
                    }
                    else if (player.Money > 50)
                    {
                        // Pay the bail

                        player.Pay(50, Bank, Bank);
                        player.FreeFromJail();
                    }
                    else
                    {
                        // No luck,  but only in jail for three turns,
                        // and will move on the next

                        player.TurnsInJail++;

                        if (player.TurnsInJail > 2)
                        {
                            player.FreeFromJail();
                        }
                    }
                }
                else // Not in jail
                {
                    // Handle three doubles 

                    if (diceResult.DoubleRolled && (++doubleCount == 3))
                    {
                        SendToJail(player);
                        bContinueMoving = false;
                    }
                    else
                    {
                        bContinueMoving = diceResult.DoubleRolled;

                        // Move the player

                        player.Location = Board.AllSquares[(player.Location.Order + diceResult.Total) % Board.AllSquares.Count];

                        // If gone past go, collect 200

                        if (player.Location.Order - diceResult.Total < 0)
                        {
                            Bank.Pay(200, player, Bank);
                        }

                        if (player.Location is AssetSquare)
                        {
                            var assetSquare = (AssetSquare)player.Location;

                            if (assetSquare.Asset.Owner is Bank)
                            {
                                player.ConsiderPurchase(assetSquare.Asset);
                            }
                            else if (player.Assets.Contains(assetSquare.Asset) == false)
                            {
                                foreach (var otherPlayer in Players.Excluding(player))
                                {
                                    if (otherPlayer.Assets.Contains(assetSquare.Asset))
                                    {
                                        if (otherPlayer.Status != PlayerStatus.InJail)
                                        {
                                            var rent = assetSquare.Asset.CalculateRent(diceResult.Total);
                                            player.Pay(rent, otherPlayer, Bank);
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        else if (player.Location is SpecialSquare)
                        {
                            LandedOnSpecialSquare(player);
                        }
                        else
                        {
                            throw new ApplicationException("Unknown square type");
                        }
                    }
                }
            }

            if (player.Status == PlayerStatus.Normal)
            {
                player.ConsiderUnmortgaging();
                player.Trade(Players);
                player.ConsiderBuilding();
            }

            NextPlayer = Players[(Players.FindIndex(x => x == player) + 1) % Players.Count];

            // Win conditions

            if (Players.Where(x => x.Status != PlayerStatus.Bankrupt).Count() == 1)
            {
                Winner = Players.Where(x => x.Status != PlayerStatus.Bankrupt).First();
                return true;
            }

            foreach (var activePlayer in ActivePlayers)
            {
                if(activePlayer.Money >= 20000)
                {
                    Winner = activePlayer;
                    return true;
                }
            }

            return false;
        }

        private void LandedOnSpecialSquare(Player player)
        {
            var specialSquare = (SpecialSquare)player.Location;
            switch (specialSquare.SpecialSquareType)
            {
                case SpecialSquare.SquareType.Go:
                    break;
                case SpecialSquare.SquareType.Jail:
                    break;
                case SpecialSquare.SquareType.FreeParking:
                    FreeParking.Pay(FreeParking.Money, player, Bank);
                    break;
                case SpecialSquare.SquareType.CommunityChest:
                    ProcessCard(player, CardManager.GetNextCommunityChest());
                    break;
                case SpecialSquare.SquareType.Chance:
                    ProcessCard(player, CardManager.GetNextChance());
                    break;
                case SpecialSquare.SquareType.GoToJail:
                    SendToJail(player);
                    break;
                case SpecialSquare.SquareType.IncomeTax:
                    player.Pay(200, FreeParking, Bank);
                    break;
                case SpecialSquare.SquareType.SuperTax:
                    player.Pay(100, FreeParking, Bank);
                    break;
                default:
                    break;
            }
        }

        private void SendToJail(Player player)
        {
            player.SetInJail();
            player.Location = Board.Jail;
        }

        private void ProcessCard(Player player, Card card)
        {
            player.LogEvent("Card:", card.GetType().Name);
            if (card is MoveCard)
            {
                var moveCard = (MoveCard)card;

                player.Location = moveCard.MoveToSquare;
            }
            else if (card is MoneyCard)
            {
                var moneyCard = (MoneyCard)card;
                if (moneyCard.Sum > 0)
                {
                    Bank.Pay(moneyCard.Sum, player, Bank);
                }
                else
                {
                    player.Pay(-moneyCard.Sum, Bank, Bank);
                }
            }
            else if (card is SpecialCard)
            {
                bool bPassedGo = false;
                var specialCard = (SpecialCard)card;

                switch (specialCard.CardType)
                {
                    case SpecialCardType.NextUtility:
                        player.Location = FindNextSquareByType(player, typeof(Utility), ref bPassedGo);
                        break;
                    case SpecialCardType.NextStation:
                        player.Location = FindNextSquareByType(player, typeof(Station), ref bPassedGo);
                        break;
                    case SpecialCardType.BackThree:
                        player.Location = Board.AllSquares[player.Location.Order - 3];
                        break;
                    case SpecialCardType.GoToJail:
                        player.Location = Board.Jail;
                        player.SetInJail();
                        break;
                    case SpecialCardType.GetOutOfJailFreeChance:
                    case SpecialCardType.GetOutOfJailFreeCommunityChest:
                        player.GetOutOfJailFreeCards.Enqueue(card);
                        break;
                    case SpecialCardType.RepairsChance:
                        {
                            var totalToPay = 0;
                            foreach (var asset in player.Assets)
                            {
                                if (asset is Property)
                                {
                                    var property = (Property)asset;

                                    var cost = 0;
                                    if (property.BuildingCount < 5)
                                    {
                                        cost = property.BuildingCount * 25;
                                    }
                                    else
                                    {
                                        cost = 100;
                                    }

                                    totalToPay += cost;
                                }
                            }

                            player.Pay(totalToPay, FreeParking, Bank);
                        }
                        break;
                    case SpecialCardType.GiveEachPlayerFifty:
                        foreach (var otherPlayer in ActivePlayers.Excluding(player))
                        {
                            player.Pay(50, otherPlayer, Bank);
                        }
                        break;
                    case SpecialCardType.ReceiveFiftyFromEachPlayer:
                        foreach (var otherPlayer in ActivePlayers.Excluding(player))
                        {
                            otherPlayer.Pay(50, player, Bank);
                        }
                        break;
                    case SpecialCardType.RepairsCommunityChest:
                        {
                            var totalToPay = 0;
                            foreach (var asset in player.Assets)
                            {
                                if (asset is Property)
                                {
                                    var property = (Property)asset;

                                    var cost = 0;
                                    if (property.BuildingCount < 5)
                                    {
                                        cost = property.BuildingCount * 40;
                                    }
                                    else
                                    {
                                        cost = 115;
                                    }

                                    totalToPay += cost;
                                }
                            }

                            player.Pay(totalToPay, FreeParking, Bank);
                        }
                        break;
                    default:
                        break;
                }

                if (bPassedGo)
                {
                    Bank.Pay(200, player, Bank);
                }
            }
            else
            {
                throw new ApplicationException("Unknown card type");
            }
        }

        private Square FindNextSquareByType(Player player, Type ta, ref bool bPassedGo)
        {
            Square squareFound = null;
            Square currentSquare = player.Location;

            while (squareFound == null)
            {
                currentSquare = Board.AllSquares[((currentSquare.Order + 1) % Board.AllSquares.Count)];

                if (currentSquare is AssetSquare)
                {
                    if (((AssetSquare)currentSquare).Asset.GetType() == ta)
                    {
                        squareFound = currentSquare;
                    }
                }
                else if (currentSquare.Name == Names.Go)
                {
                    bPassedGo = true;
                }
            }

            return squareFound;
        }

        public IEnumerable<Player> ActivePlayers
        {
            get
            {
                return Players.Where(x => x.Status != PlayerStatus.Bankrupt);
            }
        }
    }
}
