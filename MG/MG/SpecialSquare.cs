using System;
using System.Drawing;

namespace MG
{
    public class SpecialSquare : Square
    {
        public enum SquareType
        {
            Go,
            Jail,
            FreeParking,
            CommunityChest,
            Chance,
            GoToJail,
            IncomeTax,
            SuperTax,
            Out,
        }

        public SquareType SpecialSquareType { get; private set; }

        public SpecialSquare(SquareType type, int order)
            : base(order)
        {
            SpecialSquareType = type;
        }

        public override string Name
        {
            get 
            {
                switch (SpecialSquareType)
                {
                    case SquareType.Go:
                        return Names.Go;
                    case SquareType.Jail:
                        return Names.Jail;
                    case SquareType.FreeParking:
                        return Names.FreeParking;
                    case SquareType.CommunityChest:
                        return Names.CommunityChest;
                    case SquareType.Chance:
                        return Names.Chance;
                    case SquareType.GoToJail:
                        return Names.GoToJail;
                    case SquareType.IncomeTax:
                        return Names.IncomeTax;
                    case SquareType.SuperTax:
                        return Names.SuperTax;
                    case SquareType.Out:
                        return Names.Out;
                    default:
                        throw new ApplicationException("Special Square Type Name not found");
                }
            }
        }

        public override Color DisplayColor
        {
            get { return Color.Wheat; }
        }
    }
}
