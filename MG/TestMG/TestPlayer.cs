using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG
{
    [TestClass]
    public class TestPlayer
    {
        Player _player;
        Player _player2;
        Bank _bank;
        FreeParking _freeParking;
        AssetManager _assetManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _bank = new Bank();
            _assetManager = new AssetManager();
            _player = new Player();
            _player2 = new Player();
            _freeParking = new FreeParking(_bank);

            _assetManager.AllAssets.ForEach(x => _bank.PurchaseAsset(x));
        }

        [TestMethod]
        public void ConsiderPurchase_ByPreference()
        {
            var geneticCode = new GeneticCode();
            _player = new Player(geneticCode);

            geneticCode.SetGeneExpression(GeneType.PropertyValue, _assetManager.FleetStreet, 0.2f);
            geneticCode.SetGeneExpression(GeneType.PropertyValue, _assetManager.TrafalgarSquare, 0.2f);
            geneticCode.SetGeneExpression(GeneType.PropertyValue, _assetManager.TheStrand, 0.9f);

            _player.SetCashTotal(300);
            _player.ConsiderPurchase(_assetManager.TrafalgarSquare);
            _player.SetCashTotal(300);
            _player.ConsiderPurchase(_assetManager.FleetStreet);
            _player.SetCashTotal(300);
            _player.ConsiderPurchase(_assetManager.TheStrand);

            Assert.AreEqual(_player, _assetManager.TheStrand.Owner);
            Assert.AreEqual(_bank, _assetManager.FleetStreet.Owner);
            Assert.AreEqual(_bank, _assetManager.TrafalgarSquare.Owner);
        }

        [TestMethod]
        public void ConsiderBuilding_ByPreference()
        {
            var geneticCode = new GeneticCode();
            _player = new Player(geneticCode);

            geneticCode.SetGeneExpression(GeneType.ImprovementValue, _assetManager.FleetStreet, 0.1f);
            geneticCode.SetGeneExpression(GeneType.ImprovementValue, _assetManager.TrafalgarSquare, 0.2f);
            geneticCode.SetGeneExpression(GeneType.ImprovementValue, _assetManager.TheStrand, 0.3f);

            _player.PurchaseAsset(_assetManager.FleetStreet);
            _player.PurchaseAsset(_assetManager.TheStrand);
            _player.PurchaseAsset(_assetManager.TrafalgarSquare);

            // enough for 1 house
            _player.SetCashTotal(200);

            _player.ConsiderBuilding();

            Assert.AreEqual(1, _assetManager.TheStrand.BuildingCount);
            Assert.AreEqual(0, _assetManager.FleetStreet.BuildingCount);
            Assert.AreEqual(0, _assetManager.TrafalgarSquare.BuildingCount);
        }

        [TestMethod]
        public void MortgageProperties_ByPreference()
        {
            var geneticCode = new GeneticCode();
            _player = new Player(geneticCode);

            geneticCode.SetGeneExpression(GeneType.PropertyValue, _assetManager.FleetStreet, 0.1f);
            geneticCode.SetGeneExpression(GeneType.PropertyValue, _assetManager.TrafalgarSquare, 0.2f);
            geneticCode.SetGeneExpression(GeneType.PropertyValue, _assetManager.TheStrand, 0.3f);

            _player.PurchaseAsset(_assetManager.FleetStreet);
            _player.PurchaseAsset(_assetManager.TheStrand);
            _player.PurchaseAsset(_assetManager.TrafalgarSquare);

            // mortgage two properties

            _player.SetCashTotal(0);
            _player.Pay(200, _bank, _bank);

            Assert.IsTrue(_assetManager.FleetStreet.Mortgaged);
            Assert.IsTrue(_assetManager.TrafalgarSquare.Mortgaged);
            Assert.IsFalse(_assetManager.TheStrand.Mortgaged);
        }

        [TestMethod]
        public void SellsBuildings_ByPreference()
        {
            var geneticCode = new GeneticCode();
            _player = new Player(geneticCode);

            geneticCode.SetGeneExpression(GeneType.ImprovementValue, _assetManager.FleetStreet, 0.1f);
            geneticCode.SetGeneExpression(GeneType.ImprovementValue, _assetManager.TrafalgarSquare, 0.2f);
            geneticCode.SetGeneExpression(GeneType.ImprovementValue, _assetManager.TheStrand, 0.3f);

            _player.SetCashTotal(5000);

            _player.PurchaseAsset(_assetManager.FleetStreet);
            _player.PurchaseAsset(_assetManager.TheStrand);
            _player.PurchaseAsset(_assetManager.TrafalgarSquare);

            _player.PurchaseBuilding(_assetManager.FleetStreet);
            _player.PurchaseBuilding(_assetManager.TheStrand);
            _player.PurchaseBuilding(_assetManager.TrafalgarSquare);
            _player.PurchaseBuilding(_assetManager.FleetStreet);
            _player.PurchaseBuilding(_assetManager.TheStrand);
            _player.PurchaseBuilding(_assetManager.TrafalgarSquare);

            // need to sell all but one house

            _player.SetCashTotal(0);
            _player.Pay(75 * 5, _bank, _bank);

            Assert.AreEqual(1, _assetManager.TheStrand.BuildingCount);
            Assert.AreEqual(0, _assetManager.FleetStreet.BuildingCount);
            Assert.AreEqual(0, _assetManager.TrafalgarSquare.BuildingCount);
        }

        [TestMethod]
        public void UnmortgageProperties_ByPreference()
        {
            var geneticCode = new GeneticCode();
            _player = new Player(geneticCode);

            geneticCode.SetGeneExpression(GeneType.PropertyValue, _assetManager.FleetStreet, 0.1f);
            geneticCode.SetGeneExpression(GeneType.PropertyValue, _assetManager.TrafalgarSquare, 0.2f);
            geneticCode.SetGeneExpression(GeneType.PropertyValue, _assetManager.TheStrand, 0.3f);

            _player.PurchaseAsset(_assetManager.FleetStreet);
            _player.PurchaseAsset(_assetManager.TheStrand);
            _player.PurchaseAsset(_assetManager.TrafalgarSquare);

            // mortgage the properties
            _player.SetCashTotal(0);
            _player.Pay(250, _bank, _bank);

            // enough to unmortgage one
            _player.SetCashTotal(160);

            _player.ConsiderUnmortgaging();

            Assert.IsFalse(_assetManager.TheStrand.Mortgaged);
            Assert.IsTrue(_assetManager.FleetStreet.Mortgaged);
            Assert.IsTrue(_assetManager.TrafalgarSquare.Mortgaged);
        }

        [TestMethod]
        public void Purchase()
        {
            _player.SetCashTotal(325);

            _player.PurchaseAsset(_assetManager.BondStreet);

            Assert.IsTrue(_player.Assets.Contains(_assetManager.BondStreet));
            Assert.AreEqual(5, _player.Money);
            Assert.AreEqual(_player, _assetManager.BondStreet.Owner);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Purchase_OtherOwner()
        {
            var asset = new Property("test", 1, Color.Red, 1, 1, 1, 1, 1, 1, 1, 1, 1)
            {
                Owner = _player2
            };

            _player.SetCashTotal(100);

            _player.PurchaseAsset(asset);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Purchase_NotEnoughMoney()
        {
            var asset = new Property("test", 1, Color.Red, 200, 1, 1, 1, 1, 1, 1, 1, 1);

            _player.SetCashTotal(100);

            _player.PurchaseAsset(asset);
        }

        [TestMethod]
        public void SetInJail()
        {
            _player.SetInJail();

            Assert.AreEqual(PlayerStatus.InJail, _player.Status);
        }

        [TestMethod]
        public void FreeFromJail()
        {
            _player.FreeFromJail();

            Assert.AreEqual(PlayerStatus.Normal, _player.Status);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void PurchaseBuilding_DoesNotOwnProperty()
        {
            _player.SetCashTotal(1000);
            _player.PurchaseAsset(_assetManager.OldKentRoad);

            _player.PurchaseBuilding(_assetManager.Whitechapel);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void PurchaseBuilding_NoMonopoly()
        {
            _player.SetCashTotal(1000);
            _player.PurchaseAsset(_assetManager.OldKentRoad);

            _player.PurchaseBuilding(_assetManager.OldKentRoad);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void PurchaseBuilding_NotEnoughMoney()
        {
            _player.SetCashTotal(65);
            _player.PurchaseAsset(_assetManager.OldKentRoad);

            _player.PurchaseBuilding(_assetManager.OldKentRoad);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void PurchaseBuilding_NotWhileInJail()
        {
            _player.SetCashTotal(1000);
            _player.PurchaseAsset(_assetManager.OldKentRoad);
            _player.SetInJail();

            _player.PurchaseBuilding(_assetManager.OldKentRoad);
        }

        [TestMethod]
        public void PurchaseBuilding_OK()
        {
            _player.SetCashTotal(2000);
            _player.PurchaseAsset(_assetManager.OldKentRoad);
            _player.PurchaseAsset(_assetManager.Whitechapel);

            var prop = _assetManager.OldKentRoad;

            _player.PurchaseBuilding(prop);

            Assert.AreEqual(1, prop.BuildingCount);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void PurchaseBuilding_NotMoreThanHotel()
        {
            _player.SetCashTotal(2000);
            _player.PurchaseAsset(_assetManager.OldKentRoad);
            _player.PurchaseAsset(_assetManager.Whitechapel);

            var prop = _assetManager.OldKentRoad;
            _player.PurchaseBuilding(prop);
            _player.PurchaseBuilding(prop);
            _player.PurchaseBuilding(prop);
            _player.PurchaseBuilding(prop);
            _player.PurchaseBuilding(prop);

            Assert.AreEqual(5, prop.BuildingCount);

            // No more than five

            _player.PurchaseBuilding(_assetManager.OldKentRoad);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void PurchaseBuilding_CannotBuildUnevenly1()
        {
            _player.SetCashTotal(10000);
            _player.PurchaseAsset(_assetManager.TrafalgarSquare);
            _player.PurchaseAsset(_assetManager.TheStrand);
            _player.PurchaseAsset(_assetManager.FleetStreet);

            _player.PurchaseBuilding(_assetManager.TrafalgarSquare);
            _player.PurchaseBuilding(_assetManager.TrafalgarSquare);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void PurchaseBuilding_CannotBuildUnevenly2()
        {
            _player.SetCashTotal(10000);
            _player.PurchaseAsset(_assetManager.TrafalgarSquare);
            _player.PurchaseAsset(_assetManager.TheStrand);
            _player.PurchaseAsset(_assetManager.FleetStreet);

            _player.PurchaseBuilding(_assetManager.TrafalgarSquare);
            _player.PurchaseBuilding(_assetManager.TheStrand);
            _player.PurchaseBuilding(_assetManager.FleetStreet);
            _player.PurchaseBuilding(_assetManager.TrafalgarSquare);
            _player.PurchaseBuilding(_assetManager.TrafalgarSquare);
        }

        [TestMethod]
        public void PurchaseBuilding_EvenBuildingOK()
        {
            _player.SetCashTotal(10000);
            _player.PurchaseAsset(_assetManager.TrafalgarSquare);
            _player.PurchaseAsset(_assetManager.TheStrand);
            _player.PurchaseAsset(_assetManager.FleetStreet);

            _player.PurchaseBuilding(_assetManager.TrafalgarSquare);
            _player.PurchaseBuilding(_assetManager.TheStrand);
            _player.PurchaseBuilding(_assetManager.FleetStreet);
            _player.PurchaseBuilding(_assetManager.TrafalgarSquare);
            _player.PurchaseBuilding(_assetManager.TheStrand);
            _player.PurchaseBuilding(_assetManager.FleetStreet);
            _player.PurchaseBuilding(_assetManager.TrafalgarSquare);
            _player.PurchaseBuilding(_assetManager.TheStrand);
            _player.PurchaseBuilding(_assetManager.FleetStreet);
            _player.PurchaseBuilding(_assetManager.TrafalgarSquare);
            _player.PurchaseBuilding(_assetManager.TheStrand);
            _player.PurchaseBuilding(_assetManager.FleetStreet);
            _player.PurchaseBuilding(_assetManager.TrafalgarSquare);
            _player.PurchaseBuilding(_assetManager.TheStrand);
            _player.PurchaseBuilding(_assetManager.FleetStreet);

            Assert.IsTrue(_player.Assets.ToList().TrueForAll(x => ((Property)x).BuildingCount == 5));
        }

        [TestMethod]
        public void Payment_InsufficientFunds_MortgageUnimprovedProperties()
        {
            _player.SetCashTotal(2000);

            _player.PurchaseAsset(_assetManager.OldKentRoad);
            _player.PurchaseAsset(_assetManager.Whitechapel);
            _player.PurchaseAsset(_assetManager.ParkLane);
            _player.PurchaseAsset(_assetManager.Mayfair);

            _player.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.OldKentRoad, 0.1f);
            _player.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.Whitechapel, 0.2f);
            _player.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.ParkLane, 0.3f);
            _player.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.Mayfair, 0.5f);

            _player.SetCashTotal(100);

            // Not enough to pay so properties will have to be mortgaged

            _player.Pay(200, _bank, _bank);

            Assert.IsTrue(_assetManager.OldKentRoad.Mortgaged);
            Assert.IsTrue(_assetManager.Whitechapel.Mortgaged);
            Assert.IsTrue(_assetManager.ParkLane.Mortgaged);
            Assert.IsFalse(_assetManager.Mayfair.Mortgaged);
        }

        [TestMethod]
        public void Payment_Insolvent_BankRepossesses()
        {
            _player.SetCashTotal(2000);

            _player.PurchaseAsset(_assetManager.OldKentRoad);
            _player.PurchaseAsset(_assetManager.Whitechapel);

            _player.SetCashTotal(140);
            _player.GetOutOfJailFreeCards.Enqueue(new SpecialCard(SpecialCardType.GetOutOfJailFreeChance));

            // Not enough to pay so try mortgaging

            _player.Pay(200, _bank, _bank);

            Assert.IsTrue(_assetManager.OldKentRoad.Mortgaged);
            Assert.IsTrue(_assetManager.Whitechapel.Mortgaged);

            // Everything is mortgaged, so hand over properties to bank and bankrupt

            _player.Pay(50, _bank, _bank);

            Assert.AreEqual(PlayerStatus.Bankrupt, _player.Status);
            Assert.AreEqual(0, _player.Assets.Count);
            Assert.AreEqual(0, _player.Money);

            Assert.IsFalse(_assetManager.OldKentRoad.Mortgaged);
            Assert.IsFalse(_assetManager.Whitechapel.Mortgaged);
            Assert.AreEqual(_bank, _assetManager.OldKentRoad.Owner);
            Assert.AreEqual(_bank, _assetManager.Whitechapel.Owner);
            Assert.AreEqual(0, _assetManager.OldKentRoad.BuildingCount);
            Assert.AreEqual(0, _assetManager.Whitechapel.BuildingCount);
            Assert.AreEqual(0, _player.GetOutOfJailFreeCards.Count);
        }

        [TestMethod]
        public void Payment_Insolvent_FreeParking()
        {
            _player.SetCashTotal(2000);

            _player.PurchaseAsset(_assetManager.OldKentRoad);
            _player.PurchaseAsset(_assetManager.Whitechapel);

            _player.SetCashTotal(140);
            _player.GetOutOfJailFreeCards.Enqueue(new SpecialCard(SpecialCardType.GetOutOfJailFreeChance));

            // Not enough to pay so try mortgaging

            _player.Pay(200, _freeParking, _bank);

            Assert.IsTrue(_assetManager.OldKentRoad.Mortgaged);
            Assert.IsTrue(_assetManager.Whitechapel.Mortgaged);

            // Everything is mortgaged, so hand over properties to bank and bankrupt

            _player.Pay(50, _freeParking, _bank);

            Assert.AreEqual(PlayerStatus.Bankrupt, _player.Status);
            Assert.AreEqual(0, _player.Assets.Count);
            Assert.AreEqual(0, _player.Money);

            Assert.IsFalse(_assetManager.OldKentRoad.Mortgaged);
            Assert.IsFalse(_assetManager.Whitechapel.Mortgaged);
            Assert.AreEqual(_bank, _assetManager.OldKentRoad.Owner);
            Assert.AreEqual(_bank, _assetManager.Whitechapel.Owner);
            Assert.AreEqual(0, _assetManager.OldKentRoad.BuildingCount);
            Assert.AreEqual(0, _assetManager.Whitechapel.BuildingCount);
            Assert.AreEqual(0, _player.GetOutOfJailFreeCards.Count);
        }

        [TestMethod]
        public void Payment_Insolvent_PayeeTakesAll()
        {
            _player.SetCashTotal(2000);

            _player.PurchaseAsset(_assetManager.OldKentRoad);
            _player.PurchaseAsset(_assetManager.Whitechapel);

            _player.SetCashTotal(140);
            _player.GetOutOfJailFreeCards.Enqueue(new SpecialCard(SpecialCardType.GetOutOfJailFreeCommunityChest));

            // Not enough to pay so try mortgaging

            _player.Pay(200, _player2, _bank);

            Assert.IsTrue(_assetManager.OldKentRoad.Mortgaged);
            Assert.IsTrue(_assetManager.Whitechapel.Mortgaged);

            // Everything is mortgaged, so hand over properties to payee and bankrupt

            _player.Pay(50, _player2, _bank);

            Assert.AreEqual(PlayerStatus.Bankrupt, _player.Status);
            Assert.AreEqual(0, _player.Assets.Count);
            Assert.AreEqual(0, _player.Money);

            Assert.IsTrue(_player2.Assets.Contains(_assetManager.OldKentRoad));
            Assert.IsTrue(_player2.Assets.Contains(_assetManager.Whitechapel));
            Assert.IsTrue(_assetManager.OldKentRoad.Mortgaged);
            Assert.IsTrue(_assetManager.Whitechapel.Mortgaged);
            Assert.AreEqual(_player2, _assetManager.OldKentRoad.Owner);
            Assert.AreEqual(_player2, _assetManager.Whitechapel.Owner);
            Assert.AreEqual(0, _assetManager.OldKentRoad.BuildingCount);
            Assert.AreEqual(0, _assetManager.Whitechapel.BuildingCount);
        }

        [TestMethod]
        public void Payment_InsufficientFunds_CanOnlyMortgageOnce()
        {
            _player.SetCashTotal(2000);

            _player.PurchaseAsset(_assetManager.OldKentRoad);
            _player.PurchaseAsset(_assetManager.Whitechapel);
            _player.PurchaseAsset(_assetManager.ParkLane);
            _player.PurchaseAsset(_assetManager.Mayfair);

            _player.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.OldKentRoad, 0.1f);
            _player.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.Whitechapel, 0.1f);
            _player.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.ParkLane, 0.2f);
            _player.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.Mayfair, 0.5f);

            _player.SetCashTotal(100);

            // Not enough to pay so properties will have to be mortgaged

            _player.Pay(200, _bank, _bank);

            Assert.IsTrue(_assetManager.OldKentRoad.Mortgaged);
            Assert.IsTrue(_assetManager.Whitechapel.Mortgaged);
            Assert.IsTrue(_assetManager.ParkLane.Mortgaged);
            Assert.IsFalse(_assetManager.Mayfair.Mortgaged);

            _player.Pay(200, _bank, _bank);

            Assert.IsTrue(_assetManager.OldKentRoad.Mortgaged);
            Assert.IsTrue(_assetManager.Whitechapel.Mortgaged);
            Assert.IsTrue(_assetManager.ParkLane.Mortgaged);
            Assert.IsTrue(_assetManager.Mayfair.Mortgaged);

            Assert.AreEqual(135, _player.Money);
        }

        [TestMethod]
        public void Payment_InsufficientFunds_MortgageImprovedProperties1()
        {
            _player.SetCashTotal(5000);

            _player.PurchaseAsset(_assetManager.OldKentRoad);
            _player.PurchaseAsset(_assetManager.Whitechapel);
            _player.PurchaseBuilding(_assetManager.OldKentRoad);
            _player.PurchaseBuilding(_assetManager.Whitechapel);

            _player.Genetics.SetGeneExpression(GeneType.ImprovementValue, _assetManager.OldKentRoad, 0.4f);
            _player.Genetics.SetGeneExpression(GeneType.ImprovementValue, _assetManager.Whitechapel, 0.9f);

            _player.SetCashTotal(10);

            _player.Pay(20, _bank, _bank);

            // Sell a house to make the payment

            Assert.AreEqual(0, _assetManager.OldKentRoad.BuildingCount);
            Assert.AreEqual(1, _assetManager.Whitechapel.BuildingCount);

            Assert.IsFalse(_assetManager.OldKentRoad.Mortgaged);
            Assert.IsFalse(_assetManager.Whitechapel.Mortgaged);
        }

        [TestMethod]
        public void Payment_InsufficientFunds_MortgageImprovedProperties2()
        {
            _player.SetCashTotal(5000);

            _player.PurchaseAsset(_assetManager.OldKentRoad);
            _player.PurchaseAsset(_assetManager.Whitechapel);
            _player.PurchaseAsset(_assetManager.ParkLane);
            _player.PurchaseAsset(_assetManager.Mayfair);
            _player.PurchaseBuilding(_assetManager.OldKentRoad);
            _player.PurchaseBuilding(_assetManager.Whitechapel);
            _player.PurchaseBuilding(_assetManager.ParkLane);
            _player.PurchaseBuilding(_assetManager.Mayfair);
            _player.PurchaseBuilding(_assetManager.OldKentRoad);
            _player.PurchaseBuilding(_assetManager.Whitechapel);
            _player.PurchaseBuilding(_assetManager.ParkLane);
            _player.PurchaseBuilding(_assetManager.Mayfair);

            _player.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.OldKentRoad, 0.1f);
            _player.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.Whitechapel, 0.2f);
            _player.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.ParkLane, 0.3f);
            _player.Genetics.SetGeneExpression(GeneType.PropertyValue, _assetManager.Mayfair, 0.4f);

            _player.SetCashTotal(100);

            _player.Pay(790, _bank, _bank);

            // Sell all houses, then mortgage all properties, but Mayfair

            Assert.AreEqual(0, _assetManager.OldKentRoad.BuildingCount);
            Assert.AreEqual(0, _assetManager.Whitechapel.BuildingCount);
            Assert.AreEqual(0, _assetManager.ParkLane.BuildingCount);
            Assert.AreEqual(0, _assetManager.Mayfair.BuildingCount);

            Assert.IsTrue(_assetManager.OldKentRoad.Mortgaged);
            Assert.IsTrue(_assetManager.Whitechapel.Mortgaged);
            Assert.IsTrue(_assetManager.ParkLane.Mortgaged);
            Assert.IsFalse(_assetManager.Mayfair.Mortgaged);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Payment_CannotSellPropertiesUnevenly()
        {
            _player.SetCashTotal(5000);

            _player.PurchaseAsset(_assetManager.OldKentRoad);
            _player.PurchaseAsset(_assetManager.Whitechapel);
            _player.PurchaseBuilding(_assetManager.OldKentRoad);
            _player.PurchaseBuilding(_assetManager.Whitechapel);
            _player.PurchaseBuilding(_assetManager.OldKentRoad);
            _player.PurchaseBuilding(_assetManager.Whitechapel);
            _player.PurchaseBuilding(_assetManager.OldKentRoad);
            _player.PurchaseBuilding(_assetManager.Whitechapel);

            _player.SellBuilding(_assetManager.OldKentRoad);
            _player.SellBuilding(_assetManager.OldKentRoad);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Payment_CannotSellPropertiesUnevenly2()
        {
            _player.SetCashTotal(5000);

            _player.PurchaseAsset(_assetManager.FleetStreet);
            _player.PurchaseAsset(_assetManager.TheStrand);
            _player.PurchaseAsset(_assetManager.TrafalgarSquare);
            _player.PurchaseBuilding(_assetManager.FleetStreet);
            _player.PurchaseBuilding(_assetManager.TheStrand);
            _player.PurchaseBuilding(_assetManager.TrafalgarSquare);
            _player.PurchaseBuilding(_assetManager.FleetStreet);
            _player.PurchaseBuilding(_assetManager.TheStrand);
            _player.PurchaseBuilding(_assetManager.TrafalgarSquare);
            _player.PurchaseBuilding(_assetManager.FleetStreet);
            _player.PurchaseBuilding(_assetManager.TheStrand);
            _player.PurchaseBuilding(_assetManager.TrafalgarSquare);

            _player.SellBuilding(_assetManager.FleetStreet);
            _player.SellBuilding(_assetManager.FleetStreet);
        }

        [TestMethod]
        public void UnmortgageAssets()
        {
            _player.SetCashTotal(5000);

            _player.PurchaseAsset(_assetManager.FleetStreet);
            _player.PurchaseAsset(_assetManager.TheStrand);
            _player.PurchaseAsset(_assetManager.TrafalgarSquare);

            _player.SetCashTotal(0);

            _player.Pay(250, _player2, _bank);

            // All three are mortgaged.

            _player.SetCashTotal(1000);

            _player.ConsiderUnmortgaging();
        }
    }
}
