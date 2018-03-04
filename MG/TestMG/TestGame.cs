using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace MG
{
    [TestClass]
    public class TestGame
    {
        Game _game;
        Mock<IDice> _mockDice;
        Mock<ICardManager> _mockCardManager;
        List<Player> _players;
        Player _player1;
        Player _player2;
        MoneyCard _moneyCard;
        Card _getOutOfJailFreeCard;
        Bank _bank;

        Board _board;
        AssetManager _assetManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _bank = new Bank();
            _assetManager = new AssetManager();
            _board = new Board(_assetManager);
            _mockCardManager = new Mock<ICardManager>();
            _mockDice = new Mock<IDice>();
            _players = new List<Player>() { new Player(), new Player() };

            _game = new Game(_board, _bank, _mockDice.Object, _mockCardManager.Object, _players);

            _player1 = _players[0];
            _player2 = _players[1];

            _moneyCard = new MoneyCard(0);
            _getOutOfJailFreeCard = new SpecialCard(SpecialCardType.GetOutOfJailFreeCommunityChest);
        }

        [TestMethod]
        public void PlayerTurn_RollsOnceWithoutDouble()
        {
            var rollCount = 0;
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(10, false)).Callback(() => rollCount++);

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(1, rollCount);
            Assert.AreEqual(10, _player1.Location.Order);
        }

        [TestMethod]
        public void PlayerTurn_DoubleRollsAgain()
        {
            _mockDice.SetupSequence(x => x.RollDice()).Returns(new DiceResult(10, true)).Returns(new DiceResult(10, false));

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(20, _player1.Location.Order);
        }

        [TestMethod]
        public void PlayerTurn_PassGoCollect200()
        {
            // Land on Jail (no purchase)

            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(11, false));
            _player1.Location = _board.Mayfair;

            var cashBefore = _player1.Money;

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(cashBefore + 200, _player1.Money);
        }

        [TestMethod]
        public void PlayerTurn_PurchasePropertyIfPossible()
        {
            // Land on Whitechapel

            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(3, false));

            _player1.SetCashTotal(10000);

            _game.PlayPlayerTurn(_player1);

            Assert.IsTrue(_player1.Assets.Contains(_assetManager.Whitechapel));
            Assert.AreEqual(10000 - _assetManager.Whitechapel.Price, _player1.Money);
        }

        [TestMethod]
        public void PlayerTurn_NotEnoughMoneyToPurchaseProperty()
        {
            // Land on Whitechapel

            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(3, false));

            _player1.SetCashTotal(50);

            _game.PlayPlayerTurn(_player1);


            Assert.IsFalse(_player1.Assets.Contains(_assetManager.Whitechapel));
            Assert.AreEqual(50, _player1.Money);
        }

        [TestMethod]
        public void PlayerTurn_PayRent()
        {
            // Land on Whitechapel

            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(3, false));

            // Elisabeth owns Whitechapel

            _player2.PurchaseAsset(_assetManager.Whitechapel);

            // Reset cash

            _player1.SetCashTotal(100);
            _player2.SetCashTotal(100);

            _game.PlayPlayerTurn(_player1);

            Assert.IsFalse(_player1.Assets.Contains(_assetManager.Whitechapel));
            Assert.AreEqual(96, _player1.Money);
            Assert.AreEqual(104, _player2.Money);
        }

        [TestMethod]
        public void PlayerTurn_DontPayRentToCriminals()
        {
            // Land on Whitechapel

            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(3, false));

            // Elisabeth owns Whitechapel, but is in Jail

            _player2.PurchaseAsset(_assetManager.Whitechapel);
            _player2.SetInJail();

            // Reset cash

            _player1.SetCashTotal(100);
            _player2.SetCashTotal(100);

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(100, _player1.Money);
            Assert.AreEqual(100, _player2.Money);
        }

        [TestMethod]
        public void PlayerTurn_ThreeDoublesLandsYouInJail()
        {
            _mockDice.SetupSequence(x => x.RollDice()).Returns(new DiceResult(4, true)).Returns(new DiceResult(4, true)).Returns(new DiceResult(4, true));

            _player1.Location = _board.Go;

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(_board.Jail, _player1.Location);
            Assert.AreEqual(PlayerStatus.InJail, _player1.Status);
        }

        [TestMethod]
        public void PlayerTurn_CantMoveWhenInJail()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(3, false));

            _player1.Location = _board.Jail;
            _player1.SetInJail();

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(_board.Jail, _player1.Location);
        }

        [TestMethod]
        public void PlayerTurn_CantMoveWhenInJailButDoubleRolled()
        {
            _mockDice.SetupSequence(x => x.RollDice()).Returns(new DiceResult(4, true)).Returns(new DiceResult(4, false));

            _player1.Location = _board.Jail;
            _player1.SetInJail();

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(_board.Jail, _player1.Location);
            Assert.AreEqual(PlayerStatus.Normal, _player1.Status);

            // Moves out on the next go

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(_board.NorthumberlandAvenue, _player1.Location);
        }

        [TestMethod]
        public void PlayerTurn_ThreeTurnsInJail()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(4, false));

            _player1.Location = _board.Jail;
            _player1.SetInJail();
            _player1.SetCashTotal(40);

            _game.PlayPlayerTurn(_player1);
            Assert.AreEqual(PlayerStatus.InJail, _player1.Status);
            _game.PlayPlayerTurn(_player1);
            Assert.AreEqual(PlayerStatus.InJail, _player1.Status);
            _game.PlayPlayerTurn(_player1);
            Assert.AreEqual(PlayerStatus.Normal, _player1.Status);

            Assert.AreEqual(_board.Jail, _player1.Location);

            // Moves out on the next go

            _game.PlayPlayerTurn(_player1);
            Assert.AreEqual(_board.NorthumberlandAvenue, _player1.Location);
        }

        [TestMethod]
        public void PlayerTurn_PaysBailIfAffordable()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(4, false));

            _player1.Location = _board.Jail;
            _player1.SetInJail();
            _player1.SetCashTotal(500);

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(PlayerStatus.Normal, _player1.Status);
            Assert.AreEqual(_board.Jail, _player1.Location);
        }

        [TestMethod]
        public void PlayerTurn_GetOutOfJailFree()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(4, false));

            _player1.Location = _board.Jail;
            _player1.SetInJail();
            _player1.GetOutOfJailFreeCards.Enqueue(_getOutOfJailFreeCard);

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(PlayerStatus.Normal, _player1.Status);
            Assert.AreEqual(_board.Jail, _player1.Location);
            Assert.AreEqual(0, _player1.GetOutOfJailFreeCards.Count);

            _mockCardManager.Verify(x => x.ReturnGetOutOfJailFreeCard(_getOutOfJailFreeCard));

            // Moves out on the next go

            _game.PlayPlayerTurn(_player1);
            Assert.AreEqual(_board.NorthumberlandAvenue, _player1.Location);
        }

        [TestMethod]
        public void PlayerTurn_CommunityChest1()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextCommunityChest()).Returns(_moneyCard);

            _player1.Location = _board.Go;

            _game.PlayPlayerTurn(_player1);

            _mockCardManager.Verify(x => x.GetNextCommunityChest());
        }

        [TestMethod]
        public void PlayerTurn_CommunityChest2()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextCommunityChest()).Returns(_moneyCard);

            _player1.Location = _board.MaryleboneStation;

            _game.PlayPlayerTurn(_player1);

            _mockCardManager.Verify(x => x.GetNextCommunityChest());
        }

        [TestMethod]
        public void PlayerTurn_CommunityChest3()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextCommunityChest()).Returns(_moneyCard);

            _player1.Location = _board.RegentStreet;

            _game.PlayPlayerTurn(_player1);

            _mockCardManager.Verify(x => x.GetNextCommunityChest());
        }

        [TestMethod]
        public void PlayerTurn_Chance1()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextChance()).Returns(_moneyCard);

            _player1.Location = _board.KingsCrossStation;

            _game.PlayPlayerTurn(_player1);

            _mockCardManager.Verify(x => x.GetNextChance());
        }

        [TestMethod]
        public void PlayerTurn_Chance2()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextChance()).Returns(_moneyCard);

            _player1.Location = _board.FreeParking;

            _game.PlayPlayerTurn(_player1);

            _mockCardManager.Verify(x => x.GetNextChance());
        }

        [TestMethod]
        public void PlayerTurn_Chance3()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextChance()).Returns(_moneyCard);

            _player1.Location = _board.BondStreet;

            _game.PlayPlayerTurn(_player1);

            _mockCardManager.Verify(x => x.GetNextChance());
        }

        [TestMethod]
        public void PlayerTurn_FreeParking()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));

            _player1.Location = _board.MarlboroughStreet;
            _player1.SetCashTotal(100);
            _game.FreeParking.SetCashTotal(100);

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(0, _game.FreeParking.Money);
            Assert.AreEqual(200, _player1.Money);
        }

        [TestMethod]
        public void PlayerTurn_GoToJail()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));

            _player1.Location = _board.WaterWorks;

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(PlayerStatus.InJail, _player1.Status);
            Assert.AreEqual(_board.Jail, _player1.Location);
        }

        [TestMethod]
        public void PlayerTurn_IncomeTax()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(3, false));

            _player1.Location = _board.OldKentRoad;
            _player1.SetCashTotal(500);
            _game.FreeParking.SetCashTotal(0);

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(300, _player1.Money);
            Assert.AreEqual(200, _game.FreeParking.Money);
        }

        [TestMethod]
        public void PlayerTurn_SuperTax()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(3, false));

            _player1.Location = _board.LiverpoolStreetStation;
            _player1.SetCashTotal(500);
            _game.FreeParking.SetCashTotal(0);

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(400, _player1.Money);
            Assert.AreEqual(100, _game.FreeParking.Money);
        }

        [TestMethod]
        public void Cards_Give50()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextCommunityChest()).Returns(new SpecialCard(SpecialCardType.GiveEachPlayerFifty));

            _player1.Location = _board.RegentStreet;

            _player1.SetCashTotal(100);
            _player2.SetCashTotal(100);

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(150, _player2.Money);
            Assert.AreEqual(50, _player1.Money);
        }

        [TestMethod]
        public void Cards_Receive50()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextCommunityChest()).Returns(new SpecialCard(SpecialCardType.ReceiveFiftyFromEachPlayer));

            _player1.Location = _board.RegentStreet;

            _player1.SetCashTotal(100);
            _player2.SetCashTotal(100);

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(150, _player1.Money);
            Assert.AreEqual(50, _player2.Money);
        }

        [TestMethod]
        public void Cards_Give50_ExceptBankrupt()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextCommunityChest()).Returns(new SpecialCard(SpecialCardType.GiveEachPlayerFifty));

            _player1.Location = _board.RegentStreet;

            _player1.SetCashTotal(100);

            _player2.SetCashTotal(0);
            _player2.Pay(100, _game.Bank, _game.Bank);

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(100, _player1.Money);
        }

        [TestMethod]
        public void Cards_Receive50_ExceptBankrupt()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextCommunityChest()).Returns(new SpecialCard(SpecialCardType.ReceiveFiftyFromEachPlayer));

            _player1.Location = _board.RegentStreet;

            _player1.SetCashTotal(100);

            _player2.SetCashTotal(0);
            _player2.Pay(100, _game.Bank, _game.Bank);

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(100, _player1.Money);
        }

        [TestMethod]
        public void Cards_MoveCard()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextCommunityChest()).Returns(new MoveCard(_board.Whitechapel));

            _player1.Location = _board.RegentStreet;

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(_board.Whitechapel, _player1.Location);
        }

        [TestMethod]
        public void Cards_MoneyCard()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextCommunityChest()).Returns(new MoneyCard(100));

            _player1.SetCashTotal(250);
            _player1.Location = _board.RegentStreet;

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(350, _player1.Money);
        }

        [TestMethod]
        public void Cards_NextUtilityCard()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextCommunityChest()).Returns(new SpecialCard(SpecialCardType.NextUtility));

            _player1.SetCashTotal(100);
            _player1.Location = _board.RegentStreet;

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(300, _player1.Money);
            Assert.AreEqual(_board.ElectricCompany, _player1.Location);
        }

        [TestMethod]
        public void Cards_NextStation()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextCommunityChest()).Returns(new SpecialCard(SpecialCardType.NextStation));

            _player1.Location = _board.RegentStreet;

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(_board.LiverpoolStreetStation, _player1.Location);
        }

        [TestMethod]
        public void Cards_BackThree()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextCommunityChest()).Returns(new SpecialCard(SpecialCardType.BackThree));

            _player1.SetCashTotal(100);
            _player1.Location = _board.MaryleboneStation;

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(100, _player1.Money);
            Assert.AreEqual(_board.NorthumberlandAvenue, _player1.Location);
        }

        [TestMethod]
        public void Cards_GoToJail()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextCommunityChest()).Returns(new SpecialCard(SpecialCardType.GoToJail));

            _player1.Location = _board.MaryleboneStation;

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(_board.Jail, _player1.Location);
            Assert.AreEqual(PlayerStatus.InJail, _player1.Status);
        }

        [TestMethod]
        public void Cards_GetOutOfJailFreeCard()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextCommunityChest()).Returns(new SpecialCard(SpecialCardType.GetOutOfJailFreeChance));

            _player1.GetOutOfJailFreeCards.Clear();
            _player1.Location = _board.MaryleboneStation;

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(1, _player1.GetOutOfJailFreeCards.Count);
        }

        [TestMethod]
        public void Cards_RepairsChance()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextChance()).Returns(new SpecialCard(SpecialCardType.RepairsChance));

            _player1.SetCashTotal(5000);

            _player1.Location = _board.FreeParking;
            _player1.PurchaseAsset(_assetManager.OldKentRoad);
            _player1.PurchaseAsset(_assetManager.Whitechapel);
            _player1.PurchaseAsset(_assetManager.ParkLane);
            _player1.PurchaseAsset(_assetManager.Mayfair);

            _player1.PurchaseBuilding(_assetManager.OldKentRoad);
            _player1.PurchaseBuilding(_assetManager.Whitechapel);
            _player1.PurchaseBuilding(_assetManager.OldKentRoad);
            _player1.PurchaseBuilding(_assetManager.Whitechapel);
            _player1.PurchaseBuilding(_assetManager.OldKentRoad);

            _player1.PurchaseBuilding(_assetManager.Mayfair);
            _player1.PurchaseBuilding(_assetManager.ParkLane);
            _player1.PurchaseBuilding(_assetManager.Mayfair);
            _player1.PurchaseBuilding(_assetManager.ParkLane);
            _player1.PurchaseBuilding(_assetManager.Mayfair);
            _player1.PurchaseBuilding(_assetManager.ParkLane);
            _player1.PurchaseBuilding(_assetManager.Mayfair);
            _player1.PurchaseBuilding(_assetManager.ParkLane);
            _player1.PurchaseBuilding(_assetManager.Mayfair);

            _player1.SetCashTotal(330);
            _game.FreeParking.SetCashTotal(0);
            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual((25 * 9) + (100 * 1), _game.FreeParking.Money);
        }

        [TestMethod]
        public void Cards_RepairsCommunityChest()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextCommunityChest()).Returns(new SpecialCard(SpecialCardType.RepairsCommunityChest));

            _player1.SetCashTotal(5000);

            _player1.Location = _board.MaryleboneStation;

            _player1.PurchaseAsset(_assetManager.TrafalgarSquare);
            _player1.PurchaseAsset(_assetManager.TheStrand);
            _player1.PurchaseAsset(_assetManager.FleetStreet);

            _player1.PurchaseBuilding(_assetManager.TheStrand);
            _player1.PurchaseBuilding(_assetManager.TrafalgarSquare);
            _player1.PurchaseBuilding(_assetManager.FleetStreet);
            _player1.PurchaseBuilding(_assetManager.TheStrand);
            _player1.PurchaseBuilding(_assetManager.TrafalgarSquare);
            _player1.PurchaseBuilding(_assetManager.FleetStreet);
            _player1.PurchaseBuilding(_assetManager.TheStrand);
            _player1.PurchaseBuilding(_assetManager.TrafalgarSquare);
            _player1.PurchaseBuilding(_assetManager.FleetStreet);
            _player1.PurchaseBuilding(_assetManager.TheStrand);
            _player1.PurchaseBuilding(_assetManager.TrafalgarSquare);
            _player1.PurchaseBuilding(_assetManager.FleetStreet);
            _player1.PurchaseBuilding(_assetManager.TheStrand);
            _player1.PurchaseBuilding(_assetManager.FleetStreet);

            _player1.SetCashTotal(400);
            _game.FreeParking.SetCashTotal(0);
            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(((40 * 4) + (115 * 2)), _game.FreeParking.Money);
        }

        [TestMethod]
        public void PlayTurn_BuysPropertiesIfAffordable()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _mockCardManager.Setup(x => x.GetNextCommunityChest()).Returns(new SpecialCard(SpecialCardType.RepairsCommunityChest));

            _game.FreeParking.SetCashTotal(0);
            _player1.Location = _board.MarlboroughStreet;

            _player1.Genetics.SetGeneExpression(GeneType.ImprovementValue, _assetManager.TrafalgarSquare, 0.9f);
            _player1.Genetics.SetGeneExpression(GeneType.ImprovementValue, _assetManager.TheStrand, 0.7f);
            _player1.Genetics.SetGeneExpression(GeneType.ImprovementValue, _assetManager.FleetStreet, 0.7f);

            _player1.PurchaseAsset(_assetManager.TrafalgarSquare);
            _player1.PurchaseAsset(_assetManager.TheStrand);
            _player1.PurchaseAsset(_assetManager.FleetStreet);

            // enough for 4 houses
            _player1.SetCashTotal(700);
            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(1, _assetManager.TheStrand.BuildingCount);
            Assert.AreEqual(1, _assetManager.FleetStreet.BuildingCount);
            Assert.AreEqual(2, _assetManager.TrafalgarSquare.BuildingCount);
        }

        [TestMethod]
        public void PlayTurn_BankruptPlayersDoNothing()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));

            _player1.SetCashTotal(0);
            _player1.Pay(1, _game.Bank, _game.Bank);

            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(PlayerStatus.Bankrupt, _player1.Status);
        }

        [TestMethod]
        public void PlayTurn_LastPlayerWins()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));

            _player1.SetCashTotal(0);
            _player1.Pay(1, _game.Bank, _game.Bank);

            var gameOver = _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(_player2, _game.Winner);
            Assert.IsTrue(gameOver);
        }

        [TestMethod]
        public void PlayTurn_FirstPlayerTo20000Wins()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));

            _player2.PurchaseAsset(_assetManager.OldKentRoad);
            _player2.SetCashTotal(19950);
            _player2.Location = _board.Mayfair;

            // will get 200 therefore win

            var gameOver = _game.PlayPlayerTurn(_player2);

            Assert.AreEqual(_player2, _game.Winner);
            Assert.IsTrue(gameOver);
        }

        [TestMethod]
        public void PlayTurn_NoTrade()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _player1.Location = _board.MarlboroughStreet;

            _player1.PurchaseAsset(_assetManager.OldKentRoad);
            _player2.PurchaseAsset(_assetManager.Mayfair);

            _player1.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.OldKentRoad, 0.9f);
            _player1.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.Mayfair, 0.9f);
            _player1.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.OldKentRoad, 0.1f);
            _player1.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.Mayfair, 0.1f);

            _player2.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.OldKentRoad, 0.9f);
            _player2.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.Mayfair, 0.9f);
            _player2.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.OldKentRoad, 0.1f);
            _player2.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.Mayfair, 0.1f);

            _player1.SetCashTotal(2000);
            _player2.SetCashTotal(2000);

            _game.PlayPlayerTurn(_player1);

            // No trade made because no one willing let anything go

            Assert.AreEqual(_player1, _assetManager.OldKentRoad.Owner);
            Assert.AreEqual(_player2, _assetManager.Mayfair.Owner);
        }

        [TestMethod]
        public void PlayTurn_TradeInAndOut()
        {
            _mockDice.Setup(x => x.RollDice()).Returns(new DiceResult(2, false));
            _player1.Location = _board.MarlboroughStreet;

            _player1.PurchaseAsset(_assetManager.OldKentRoad);
            _player2.PurchaseAsset(_assetManager.Whitechapel);
            _player1.PurchaseAsset(_assetManager.ParkLane);
            _player2.PurchaseAsset(_assetManager.Mayfair);

            _player1.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.OldKentRoad, 0.9f);
            _player1.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.Whitechapel, 0.9f);
            _player1.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.ParkLane, 0.1f);
            _player1.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.Mayfair, 0.1f);

            _player2.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.OldKentRoad, 0.1f);
            _player2.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.Whitechapel, 0.1f);
            _player2.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.ParkLane, 0.9f);
            _player2.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.Mayfair, 0.9f);

            _player1.SetCashTotal(2000);
            _player2.SetCashTotal(2000);
            
            _game.PlayPlayerTurn(_player1);

            Assert.AreEqual(_player1, _assetManager.OldKentRoad.Owner);
            Assert.AreEqual(_player1, _assetManager.Whitechapel.Owner);
            Assert.AreEqual(_player2, _assetManager.ParkLane.Owner);
            Assert.AreEqual(_player2, _assetManager.Mayfair.Owner);
        }
    }
}
