using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace MG
{
    [TestClass]
    public class TestAssets
    {
        AssetManager _assetManager;
        Bank _bank;
        Player _player;
        Property _oldKentRoadProperty;
        Property _whitechapelProperty;
        Station _station;
        Utility _utility;

        [TestInitialize]
        public void TestInitialize()
        {
            _bank = new Bank();
            _player = new Player();
            _assetManager = new AssetManager();

            _assetManager.AllAssets.ForEach(x => _bank.PurchaseAsset(x));

            _oldKentRoadProperty = _assetManager.OldKentRoad;
            _whitechapelProperty = _assetManager.Whitechapel;
            _station = _assetManager.KingsCrossStation;
            _utility = _assetManager.ElectricCompany;

            _player.SetCashTotal(2000);
            _player.PurchaseAsset(_oldKentRoadProperty);
        }

        [TestMethod]
        public void Property_CalculateRent()
        {
            var rent = _oldKentRoadProperty.CalculateRent(10);
            Assert.AreEqual(2, rent);
        }

        [TestMethod]
        public void Station_CalculateRent()
        {
            _player.PurchaseAsset(_station);
            var rent = _station.CalculateRent(10);
            Assert.AreEqual(25, rent);

            _player.PurchaseAsset(_station.MonopolySet[1]);
            rent = _station.CalculateRent(10);
            Assert.AreEqual(50, rent);

            _player.PurchaseAsset(_station.MonopolySet[2]);
            rent = _station.CalculateRent(10);
            Assert.AreEqual(100, rent);

            _player.PurchaseAsset(_station.MonopolySet[3]);
            rent = _station.CalculateRent(10);
            Assert.AreEqual(200, rent);
        }

        [TestMethod]
        public void Utility_CalculateRent()
        {
            _player.PurchaseAsset(_utility);
            var rent = _utility.CalculateRent(10);
            Assert.AreEqual(4 * 10, rent);

            _player.PurchaseAsset(_utility.MonopolySet[1]);
            rent = _utility.CalculateRent(10);
            Assert.AreEqual(10 * 10, rent);
        }

        [TestMethod]
        public void Property_CalculateRentWithMonopoly()
        {
            // Own all the linked properties
            _player.PurchaseAsset(_whitechapelProperty);

            var rent = _oldKentRoadProperty.CalculateRent(10);
            Assert.AreEqual(4, rent);
        }

        [TestMethod]
        public void Property_CalculateRentWithHouses()
        {
            // Own all the linked properties
            _player.PurchaseAsset(_whitechapelProperty);

            _player.PurchaseBuilding(_oldKentRoadProperty);
            _player.PurchaseBuilding(_whitechapelProperty);
            Assert.AreEqual(10, _oldKentRoadProperty.CalculateRent(10));

            _player.PurchaseBuilding(_oldKentRoadProperty);
            _player.PurchaseBuilding(_whitechapelProperty);
            Assert.AreEqual(30, _oldKentRoadProperty.CalculateRent(10));

            _player.PurchaseBuilding(_oldKentRoadProperty);
            _player.PurchaseBuilding(_whitechapelProperty);
            Assert.AreEqual(90, _oldKentRoadProperty.CalculateRent(10));

            _player.PurchaseBuilding(_oldKentRoadProperty);
            _player.PurchaseBuilding(_whitechapelProperty);
            Assert.AreEqual(160, _oldKentRoadProperty.CalculateRent(10));

            _player.PurchaseBuilding(_oldKentRoadProperty);
            _player.PurchaseBuilding(_whitechapelProperty);
            Assert.AreEqual(250, _oldKentRoadProperty.CalculateRent(10));
        }
    }
}
