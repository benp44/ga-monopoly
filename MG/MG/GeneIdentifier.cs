using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG
{
    public class GeneIdentifier
    {
        public Asset Asset { get; set; }
        public GeneType GeneType { get; set; }

        public GeneIdentifier(Asset asset, GeneType geneType)
        {
            Asset = asset;
            GeneType = geneType;
        }

        public override bool Equals(object obj)
        {
            if (obj is GeneIdentifier)
            {
                return obj.GetHashCode() == GetHashCode();
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (100 * Asset.Order) + (int)GeneType;
        }
    }
}
