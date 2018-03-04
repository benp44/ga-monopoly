using System.Collections.Generic;

namespace MG
{
    public class Board
    {
        public SpecialSquare Go  { get; private set; }
        public AssetSquare OldKentRoad  { get; private set; }
        public SpecialSquare CommunityChest1  { get; private set; }
        public AssetSquare Whitechapel  { get; private set; }
        public SpecialSquare IncomeTax  { get; private set; }
        public AssetSquare KingsCrossStation  { get; private set; }
        public AssetSquare TheAngelIslington  { get; private set; }
        public SpecialSquare Chance1  { get; private set; }
        public AssetSquare EustonRoad  { get; private set; }
        public AssetSquare PentonvilleRoad  { get; private set; }
        public SpecialSquare Jail  { get; private set; }
        public AssetSquare PallMall  { get; private set; }
        public AssetSquare ElectricCompany  { get; private set; }
        public AssetSquare Whitehall  { get; private set; }
        public AssetSquare NorthumberlandAvenue  { get; private set; }
        public AssetSquare MaryleboneStation  { get; private set; }
        public AssetSquare BowStreet  { get; private set; }
        public SpecialSquare CommunityChest2  { get; private set; }
        public AssetSquare MarlboroughStreet  { get; private set; }
        public AssetSquare VineStreet  { get; private set; }
        public SpecialSquare FreeParking  { get; private set; }
        public AssetSquare TheStrand  { get; private set; }
        public SpecialSquare Chance2  { get; private set; }
        public AssetSquare FleetStreet  { get; private set; }
        public AssetSquare TrafalgarSquare  { get; private set; }
        public AssetSquare FenchurchStStation  { get; private set; }
        public AssetSquare LeicesterSquare  { get; private set; }
        public AssetSquare CoventryStreet  { get; private set; }
        public AssetSquare WaterWorks  { get; private set; }
        public AssetSquare Piccadilly  { get; private set; }
        public SpecialSquare GoToJail  { get; private set; }
        public AssetSquare RegentStreet  { get; private set; }
        public AssetSquare OxfordStreet  { get; private set; }
        public SpecialSquare CommunityChest3  { get; private set; }
        public AssetSquare BondStreet  { get; private set; }
        public AssetSquare LiverpoolStreetStation  { get; private set; }
        public SpecialSquare Chance3  { get; private set; }
        public AssetSquare ParkLane  { get; private set; }
        public SpecialSquare SuperTax  { get; private set; }
        public AssetSquare Mayfair  { get; private set; }

        public List<Square> AllSquares { get; private set; }
        public AssetManager AssetManager { get; private set; }

        public Board(AssetManager assetManager)
        {
            AssetManager = assetManager;
            Reset();
        }

        private void Reset()
        {
            Go = new SpecialSquare(SpecialSquare.SquareType.Go, 0);
            OldKentRoad = new AssetSquare(AssetManager.OldKentRoad, 1);
            CommunityChest1 = new SpecialSquare(SpecialSquare.SquareType.CommunityChest, 2);
            Whitechapel = new AssetSquare(AssetManager.Whitechapel, 3);
            IncomeTax = new SpecialSquare(SpecialSquare.SquareType.IncomeTax, 4);
            KingsCrossStation = new AssetSquare(AssetManager.KingsCrossStation, 5);
            TheAngelIslington = new AssetSquare(AssetManager.TheAngelIslington, 6);
            Chance1 = new SpecialSquare(SpecialSquare.SquareType.Chance, 7);
            EustonRoad = new AssetSquare(AssetManager.EustonRoad, 8);
            PentonvilleRoad = new AssetSquare(AssetManager.PentonvilleRoad, 9);
            Jail = new SpecialSquare(SpecialSquare.SquareType.Jail, 10);
            PallMall = new AssetSquare(AssetManager.PallMall, 11);
            ElectricCompany = new AssetSquare(AssetManager.ElectricCompany, 12);
            Whitehall = new AssetSquare(AssetManager.Whitehall, 13);
            NorthumberlandAvenue = new AssetSquare(AssetManager.NorthumberlandAvenue, 14);
            MaryleboneStation = new AssetSquare(AssetManager.MaryleboneStation, 15);
            BowStreet = new AssetSquare(AssetManager.BowStreet, 16);
            CommunityChest2 = new SpecialSquare(SpecialSquare.SquareType.CommunityChest, 17);
            MarlboroughStreet = new AssetSquare(AssetManager.MarlboroughStreet, 18);
            VineStreet = new AssetSquare(AssetManager.VineStreet, 19);
            FreeParking = new SpecialSquare(SpecialSquare.SquareType.FreeParking, 20);
            TheStrand = new AssetSquare(AssetManager.TheStrand, 21);
            Chance2 = new SpecialSquare(SpecialSquare.SquareType.Chance, 22);
            FleetStreet = new AssetSquare(AssetManager.FleetStreet, 23);
            TrafalgarSquare = new AssetSquare(AssetManager.TrafalgarSquare, 24);
            FenchurchStStation = new AssetSquare(AssetManager.FenchurchStStation, 25);
            LeicesterSquare = new AssetSquare(AssetManager.LeicesterSquare, 26);
            CoventryStreet = new AssetSquare(AssetManager.CoventryStreet, 27);
            WaterWorks = new AssetSquare(AssetManager.WaterWorks, 28);
            Piccadilly = new AssetSquare(AssetManager.Piccadilly, 29);
            GoToJail = new SpecialSquare(SpecialSquare.SquareType.GoToJail, 30);
            RegentStreet = new AssetSquare(AssetManager.RegentStreet, 31);
            OxfordStreet = new AssetSquare(AssetManager.OxfordStreet, 32);
            CommunityChest3 = new SpecialSquare(SpecialSquare.SquareType.CommunityChest, 33);
            BondStreet = new AssetSquare(AssetManager.BondStreet, 34);
            LiverpoolStreetStation = new AssetSquare(AssetManager.LiverpoolStreetStation, 35);
            Chance3 = new SpecialSquare(SpecialSquare.SquareType.Chance, 36);
            ParkLane = new AssetSquare(AssetManager.ParkLane, 37);
            SuperTax = new SpecialSquare(SpecialSquare.SquareType.SuperTax, 38);
            Mayfair = new AssetSquare(AssetManager.Mayfair, 39);

            AllSquares = new List<Square>();

            AllSquares.Add(Go);
            AllSquares.Add(OldKentRoad);
            AllSquares.Add(CommunityChest1);
            AllSquares.Add(Whitechapel);
            AllSquares.Add(IncomeTax);
            AllSquares.Add(KingsCrossStation);
            AllSquares.Add(TheAngelIslington);
            AllSquares.Add(Chance1);
            AllSquares.Add(EustonRoad);
            AllSquares.Add(PentonvilleRoad);
            AllSquares.Add(Jail);
            AllSquares.Add(PallMall);
            AllSquares.Add(ElectricCompany);
            AllSquares.Add(Whitehall);
            AllSquares.Add(NorthumberlandAvenue);
            AllSquares.Add(MaryleboneStation);
            AllSquares.Add(BowStreet);
            AllSquares.Add(CommunityChest2);
            AllSquares.Add(MarlboroughStreet);
            AllSquares.Add(VineStreet);
            AllSquares.Add(FreeParking);
            AllSquares.Add(TheStrand);
            AllSquares.Add(Chance2);
            AllSquares.Add(FleetStreet);
            AllSquares.Add(TrafalgarSquare);
            AllSquares.Add(FenchurchStStation);
            AllSquares.Add(LeicesterSquare);
            AllSquares.Add(CoventryStreet);
            AllSquares.Add(WaterWorks);
            AllSquares.Add(Piccadilly);
            AllSquares.Add(GoToJail);
            AllSquares.Add(RegentStreet);
            AllSquares.Add(OxfordStreet);
            AllSquares.Add(CommunityChest3);
            AllSquares.Add(BondStreet);
            AllSquares.Add(LiverpoolStreetStation);
            AllSquares.Add(Chance3);
            AllSquares.Add(ParkLane);
            AllSquares.Add(SuperTax);
            AllSquares.Add(Mayfair);
        }
    }
}
