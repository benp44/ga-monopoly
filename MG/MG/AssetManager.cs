using System.Collections.Generic;
using System.Drawing;

namespace MG
{
    public class AssetManager
    {
        public Property OldKentRoad { get; private set; }
        public Property Whitechapel { get; private set; }
        public Property TheAngelIslington { get; private set; }
        public Property EustonRoad { get; private set; }
        public Property PentonvilleRoad { get; private set; }
        public Property PallMall { get; private set; }
        public Property Whitehall { get; private set; }
        public Property NorthumberlandAvenue { get; private set; }
        public Property BowStreet { get; private set; }
        public Property MarlboroughStreet { get; private set; }
        public Property VineStreet { get; private set; }
        public Property TheStrand { get; private set; }
        public Property FleetStreet { get; private set; }
        public Property TrafalgarSquare { get; private set; }
        public Property LeicesterSquare { get; private set; }
        public Property CoventryStreet { get; private set; }
        public Property Piccadilly { get; private set; }
        public Property RegentStreet { get; private set; }
        public Property OxfordStreet { get; private set; }
        public Property BondStreet { get; private set; }
        public Property ParkLane { get; private set; }
        public Property Mayfair { get; private set; }

        public Station KingsCrossStation { get; private set; }
        public Station MaryleboneStation { get; private set; }
        public Station FenchurchStStation { get; private set; }
        public Station LiverpoolStreetStation { get; private set; }

        public Utility ElectricCompany { get; private set; }
        public Utility WaterWorks { get; private set; }

        public List<Asset> AllAssets { get; private set; }

        public AssetManager()
        {
            Reset();
        }

        private void Reset()
        {
            OldKentRoad             = new Property(Names.OldKentRoad,           0, Color.Brown,         60, 30, 30, 2, 10, 30, 90, 160, 250);
            Whitechapel             = new Property(Names.Whitechapel,           1, Color.Brown,         60, 30, 30, 4, 20, 60, 180, 320, 450);
            TheAngelIslington       = new Property(Names.TheAngelIslington,     2, Color.LightBlue,     100, 50, 50, 6, 30, 90, 270, 400, 550);
            EustonRoad              = new Property(Names.EustonRoad,            3, Color.LightBlue,     100, 50, 50, 6, 30, 90, 270, 400, 550);
            PentonvilleRoad         = new Property(Names.PentonvilleRoad,       4, Color.LightBlue,     120, 60, 50, 8, 40, 100, 300, 450, 600);
            PallMall                = new Property(Names.PallMall,              5, Color.Pink,          140, 70, 100, 10, 50, 150, 450, 625, 750);
            Whitehall               = new Property(Names.Whitehall,             6, Color.Pink,          140, 70, 100, 10, 50, 150, 450, 625, 750);
            NorthumberlandAvenue    = new Property(Names.NorthumberlandAvenue,  7, Color.Pink,          160, 80, 100, 12, 60, 180, 500, 700, 900);
            BowStreet               = new Property(Names.BowStreet,             8, Color.Orange,        180, 90, 100, 14, 70, 200, 550, 750, 950);
            MarlboroughStreet       = new Property(Names.MarlboroughStreet,     9, Color.Orange,        180, 90, 100, 14, 70, 200, 550, 750, 950);
            VineStreet              = new Property(Names.VineStreet,            10, Color.Orange,       200, 100, 100, 16, 80, 220, 600, 800, 1000);
            TheStrand               = new Property(Names.TheStrand,             11, Color.Red,          220, 110, 150, 18, 90, 250, 700, 875, 1050);
            FleetStreet             = new Property(Names.FleetStreet,           12, Color.Red,          220, 110, 150, 18, 90, 250, 700, 875, 1050);
            TrafalgarSquare         = new Property(Names.TrafalgarSquare,       13, Color.Red,          240, 120, 150, 20, 100, 300, 750, 925, 1100);
            LeicesterSquare         = new Property(Names.LeicesterSquare,       14, Color.Yellow,       260, 130, 150, 22, 110, 330, 800, 975, 1150);
            CoventryStreet          = new Property(Names.CoventryStreet,        15, Color.Yellow,       260, 130, 150, 22, 110, 330, 800, 975, 1150);
            Piccadilly              = new Property(Names.Piccadilly,            16, Color.Yellow,       280, 140, 150, 22, 120, 360, 850, 1025, 1200);
            RegentStreet            = new Property(Names.RegentStreet,          17, Color.DarkGreen,    300, 150, 150, 26, 130, 390, 900, 1100, 1275);
            OxfordStreet            = new Property(Names.OxfordStreet,          18, Color.DarkGreen,    300, 150, 150, 26, 130, 390, 900, 1100, 1275);
            BondStreet              = new Property(Names.BondStreet,            19, Color.DarkGreen,    320, 160, 150, 28, 150, 450, 1000, 1200, 1400);
            ParkLane                = new Property(Names.ParkLane,              20, Color.Purple,       350, 175, 200, 35, 175, 500, 1100, 1300, 1500);
            Mayfair                 = new Property(Names.Mayfair,               21, Color.Purple,       400, 200, 200, 50, 200, 600, 1400, 1700, 2000);
                                      
            KingsCrossStation       = new Station(Names.KingsCrossStation,      22, 200, 100);
            MaryleboneStation       = new Station(Names.MaryleboneStation,      23, 200, 100);
            FenchurchStStation      = new Station(Names.FenchurchStStation,     24, 200, 100);
            LiverpoolStreetStation  = new Station(Names.LiverpoolStreetStation, 25, 200, 100);
                                       
            ElectricCompany         = new Utility(Names.ElectricCompany,        26, 150, 75);
            WaterWorks              = new Utility(Names.WaterWorks,             27, 150, 75);

            AllAssets = new List<Asset>();

            AllAssets.Add(OldKentRoad);
            AllAssets.Add(Whitechapel);
            AllAssets.Add(TheAngelIslington);
            AllAssets.Add(EustonRoad);
            AllAssets.Add(PentonvilleRoad);
            AllAssets.Add(PallMall);
            AllAssets.Add(Whitehall);
            AllAssets.Add(NorthumberlandAvenue);
            AllAssets.Add(BowStreet);
            AllAssets.Add(MarlboroughStreet);
            AllAssets.Add(VineStreet);
            AllAssets.Add(TheStrand);
            AllAssets.Add(FleetStreet);
            AllAssets.Add(TrafalgarSquare);
            AllAssets.Add(LeicesterSquare);
            AllAssets.Add(CoventryStreet);
            AllAssets.Add(Piccadilly);
            AllAssets.Add(RegentStreet);
            AllAssets.Add(OxfordStreet);
            AllAssets.Add(BondStreet);
            AllAssets.Add(ParkLane);
            AllAssets.Add(Mayfair);
            
            AllAssets.Add(KingsCrossStation);
            AllAssets.Add(MaryleboneStation);
            AllAssets.Add(FenchurchStStation);
            AllAssets.Add(LiverpoolStreetStation);
            
            AllAssets.Add(ElectricCompany);
            AllAssets.Add(WaterWorks);

            BuildMonopolySets();
        }

        private void BuildMonopolySets()
        {
            foreach (var asset in AllAssets)
            {
                if (asset is Property)
                {
                    asset.MonopolySet.AddRange(AllAssets.FindAll(a => (a is Property) && (a.AssetColor == asset.AssetColor)));
                }
                else if (asset is Utility)
                {
                    asset.MonopolySet.AddRange(AllAssets.FindAll(a => a is Utility));
                }
                else if (asset is Station)
                {
                    asset.MonopolySet.AddRange(AllAssets.FindAll(a => a is Station));
                }
            }
        }
    }
}
