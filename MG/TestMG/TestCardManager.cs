using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MG
{
    [TestClass]
    public class TestCardManager
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void GetOutOfJailFreeCardsAreRemoved()
        {
            var cardManager = new CardManager(new Board(new AssetManager()));
            var cardsRemoved = 0;

            var chanceCount = cardManager.ChanceDeck.Count;
            var communityChestCount = cardManager.CommunityChestDeck.Count;

            for (int i = 0; i < cardManager.ChanceDeck.Count; i++)
            {
                var card = cardManager.GetNextChance();

                if (card is SpecialCard)
                { 
                    if (((SpecialCard)card).CardType == SpecialCardType.GetOutOfJailFreeChance)
                    {
                        cardsRemoved++;
                    }
                }
            }

            for (int i = 0; i < cardManager.CommunityChestDeck.Count; i++)
            {
                var card = cardManager.GetNextCommunityChest();

                if (card is SpecialCard)
                {
                    if (((SpecialCard)card).CardType == SpecialCardType.GetOutOfJailFreeCommunityChest)
                    {
                        cardsRemoved++;
                    }
                }
            }

            Assert.AreEqual(2, cardsRemoved);
            Assert.AreEqual(2, cardsRemoved);

            Assert.AreEqual(chanceCount - 1, cardManager.ChanceDeck.Count);
            Assert.AreEqual(communityChestCount - 1, cardManager.CommunityChestDeck.Count);
        }
    }
}
