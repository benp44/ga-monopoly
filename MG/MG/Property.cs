using System;
using System.Drawing;

namespace MG
{
    public class Property : Asset
    {
        public int[] Rent { get; private set; }
        public int BuildingCost { get; private set; }
        public int BuildingCount { get; private set; }

        private Color _color;

        public Property(string name, int order, Color color, int price, int mortgageValue, int buildingCost, int rentEmpty, int rent1, int rent2, int rent3, int rent4, int rentHotel)
            : base(name, order, price, mortgageValue)
        {
            _color = color;
            BuildingCount = 0;
            BuildingCost = buildingCost;

            Rent = new int[6];
            Rent[0] = rentEmpty;
            Rent[1] = rent1;
            Rent[2] = rent2;
            Rent[3] = rent3;
            Rent[4] = rent4;
            Rent[5] = rentHotel;
        }

        public override Color AssetColor
        {
            get { return _color; }
        }

        public override int CalculateRent(int diceRoll)
        {
            if (BuildingCount == 0)
            {
                if (OwnerHasMonopoly)
                {
                    return Rent[0] * 2;
                }
            }

            return Rent[BuildingCount];
        }

        public bool BuildingPossible
        {
            get
            {
                return OwnerHasMonopoly && MonopolySet.TrueForAll(m => ((Property)m).BuildingCount >= this.BuildingCount);
            }
        }

        public int Improve()
        {
            if (BuildingCount == 5)
            {
                throw new ApplicationException("Hotel already built");
            }

            BuildingCount++;
            return BuildingCost;
        }

        public int SellBuilding()
        {
            if (BuildingCount == 0)
            {
                throw new ApplicationException("No buildings to sell");
            }

            BuildingCount--;
            return (BuildingCost / 2);
        }
    }
}
