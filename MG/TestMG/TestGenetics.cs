using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MG
{
    [TestClass]
    public class TestGenetics
    {
        [TestMethod]
        public void Cross_SwapsGenes()
        {
            var geneticCode1 = new GeneticCode(10);
            var geneticCode2 = new GeneticCode(14);

            geneticCode1.SetGeneExpression(GeneType.Mortgage, AssetReference.BondStreet, 0.2f);
            geneticCode2.SetGeneExpression(GeneType.Mortgage, AssetReference.RegentStreet, 0.8f);
            geneticCode1.SetGeneExpression(GeneType.Mortgage, AssetReference.OldKentRoad, 0.7f);
            geneticCode2.SetGeneExpression(GeneType.Mortgage, AssetReference.OldKentRoad, 0.3f);
            geneticCode1.SetGeneExpression(GeneType.Mortgage, AssetReference.Whitehall, 0.1f);
            geneticCode2.SetGeneExpression(GeneType.Mortgage, AssetReference.Whitehall, 0.3f);
            geneticCode1.SetGeneExpression(GeneType.Mortgage, AssetReference.Mayfair, 0.4f);
            geneticCode2.SetGeneExpression(GeneType.Mortgage, AssetReference.Mayfair, 0.4f);

            var offspring = geneticCode1.Cross(geneticCode2, false);

            Assert.AreEqual(15, offspring.Generation);
            Assert.IsTrue(0.7f == offspring.GetGeneExpression(GeneType.Mortgage, AssetReference.OldKentRoad) || 0.3f == offspring.GetGeneExpression(GeneType.Mortgage, AssetReference.OldKentRoad));
            Assert.IsTrue(0.1f == offspring.GetGeneExpression(GeneType.Mortgage, AssetReference.Whitehall) || 0.3f == offspring.GetGeneExpression(GeneType.Mortgage, AssetReference.Whitehall));
            Assert.IsTrue(0.4f == offspring.GetGeneExpression(GeneType.Mortgage, AssetReference.Mayfair));
            Assert.IsTrue(0.2f == offspring.GetGeneExpression(GeneType.Mortgage, AssetReference.BondStreet));
            Assert.IsTrue(0.8f == offspring.GetGeneExpression(GeneType.Mortgage, AssetReference.RegentStreet));
        }
    }
}
