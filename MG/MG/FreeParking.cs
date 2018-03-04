using System;
using System.Collections.Generic;

namespace MG
{
    public class FreeParking : IAssetHolder
    {
        private int _money;
        private Bank _bank;

        public FreeParking(Bank bank)
        {
            _bank = bank;
            _money = 0;
        }

        public List<Asset> Assets
        {
            get
            {
                // hand over properties to the bank

                return _bank.Assets;
            }
        }

        public int Money
        {
            get
            {
                return _money;
            }
        }

        public IEnumerable<Property> Properties
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<Property> PropertiesWithMonopoly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Pay(int amount, IAssetHolder payee, Bank bank)
        {
            payee.Receive(amount);
            _money -= amount;
        }

        public void PurchaseAsset(Asset asset)
        {
            throw new NotImplementedException();
        }

        public void Receive(int amount)
        {
            _money += amount;
        }

        public void SetCashTotal(int total)
        {
            _money = total;
        }
    }
}