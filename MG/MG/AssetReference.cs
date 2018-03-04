using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG
{
    public static class AssetReference 
    {
        static AssetManager _assetManager;

        static AssetReference()
        {
            _assetManager = new AssetManager();
        }

        public static List<Asset> AssetList
        {
            get
            {
                return _assetManager.AllAssets;
            }
        }

        public static Property OldKentRoad { get { return _assetManager.OldKentRoad; }}
        public static Property Whitechapel { get { return _assetManager.Whitechapel; }}
        public static Property TheAngelIslington { get { return _assetManager.TheAngelIslington; }}
        public static Property EustonRoad { get { return _assetManager.EustonRoad; }}
        public static Property PentonvilleRoad { get { return _assetManager.PentonvilleRoad; }}
        public static Property PallMall { get { return _assetManager.PallMall; }}
        public static Property Whitehall { get { return _assetManager.Whitehall; }}
        public static Property NorthumberlandAvenue { get { return _assetManager.NorthumberlandAvenue; }}
        public static Property BowStreet { get { return _assetManager.BowStreet; }}
        public static Property MarlboroughStreet { get { return _assetManager.MarlboroughStreet; }}
        public static Property VineStreet { get { return _assetManager.VineStreet; }}
        public static Property TheStrand { get { return _assetManager.TheStrand; }}
        public static Property FleetStreet { get { return _assetManager.FleetStreet; }}
        public static Property TrafalgarSquare { get { return _assetManager.TrafalgarSquare; }}
        public static Property LeicesterSquare { get { return _assetManager.LeicesterSquare; }}
        public static Property CoventryStreet { get { return _assetManager.CoventryStreet; }}
        public static Property Piccadilly { get { return _assetManager.Piccadilly; }}
        public static Property RegentStreet { get { return _assetManager.RegentStreet; }}
        public static Property OxfordStreet { get { return _assetManager.OxfordStreet; }}
        public static Property BondStreet { get { return _assetManager.BondStreet; }}
        public static Property ParkLane { get { return _assetManager.ParkLane; }}
        public static Property Mayfair { get { return _assetManager.Mayfair; }}
                
        public static Station KingsCrossStation { get { return _assetManager.KingsCrossStation; } }
        public static Station MaryleboneStation { get { return _assetManager.MaryleboneStation; } }
        public static Station FenchurchStStation { get { return _assetManager.FenchurchStStation; } }
        public static Station LiverpoolStreetStation { get { return _assetManager.LiverpoolStreetStation; } }

        public static Utility ElectricCompany { get { return _assetManager.ElectricCompany; } }
        public static Utility WaterWorks { get { return _assetManager.WaterWorks; } }
    }
}
