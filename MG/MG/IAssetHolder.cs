using System.Collections.Generic;

namespace MG
{
    public interface IAssetHolder
    {
        void Pay(int amount, IAssetHolder payee, Bank bank);
        void Receive(int amount);

        int Money { get; }
        void SetCashTotal(int total);

        void PurchaseAsset(Asset asset);
        List<Asset> Assets { get; }
        IEnumerable<Property> Properties { get; }
        IEnumerable<Property> PropertiesWithMonopoly { get; }
    }
}
