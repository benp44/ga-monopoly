using System;
using System.Collections.Generic;

namespace MG
{
    public class Bank : IAssetHolder
    {
        public List<Asset> Assets { get; private set;  }

        public Bank()
        {
            Assets = new List<Asset>();
        }

        public int Money
        {
            get
            {
                throw new NotImplementedException();
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

        public void PurchaseAsset(Asset asset)
        {
            Assets.Add(asset);
            asset.Owner = this;
        }

        public void Pay(int amount, IAssetHolder payee, Bank bank)
        {
            payee.Receive(amount);
        }

        public void Receive(int amount)
        {
        }

        public void SetCashTotal(int total)
        {
            throw new NotImplementedException();
        }
    }
}