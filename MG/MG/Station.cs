using System;

namespace MG
{
    public class Station : Asset
    {
        public Station(string name, int order, int price, int mortgageValue)
            : base (name, order, price, mortgageValue)
        {
        }

        public override System.Drawing.Color AssetColor
        {
            get { return System.Drawing.Color.Black; }
        }

        public override int CalculateRent(int diceRoll)
        {
            var stationCount = 0;

            foreach (var m in MonopolySet)
            {
                if (Owner.Assets.Contains(m) == true)
                {
                    stationCount++;
                }
            }

            return 25 * (int)(Math.Pow(2, (stationCount - 1)));
        }
    }
}
