using System;

namespace MG
{
    public class Utility : Asset
    {
        public Utility(string name, int order, int price, int mortgageValue)
            : base (name, order, price, mortgageValue)
        {
        }

        public override System.Drawing.Color AssetColor
        {
            get { return System.Drawing.Color.Gray; }
        }

        public override int CalculateRent(int diceRoll)
        {
            var utilityCount = 0;

            foreach (var m in MonopolySet)
            {
                if (Owner.Assets.Contains(m))
                {
                    utilityCount++;
                }
            }

            if (utilityCount == 1)
            {
                return diceRoll * 4;
            }
            else if (utilityCount == 2)
            {
                return diceRoll * 10;
            }
            else
            {
                throw new ApplicationException("something bad happened counting utilties");
            }
        }
    }
}
