using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MG;

namespace TestMG
{
    [TestClass]
    public class TestDice
    {
        [TestMethod]
        public void RollDice()
        {
            var dice = new Dice();

            for (int i = 0; i < 100; i++)
            {
                var result = dice.RollDice();
                Assert.IsTrue(result.Total > 1 && result.Total < 13);
                if (result.DoubleRolled)
                {
                    Assert.IsTrue(result.Total % 2 == 0);
                }
            }
        }
    }
}
