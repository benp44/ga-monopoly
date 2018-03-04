using System.Collections.Generic;

namespace MG
{
    public interface ICardManager
    {
        Queue<Card> ChanceDeck { get; }
        Queue<Card> CommunityChestDeck { get; }

        Card GetNextChance();
        Card GetNextCommunityChest();
        void ReturnGetOutOfJailFreeCard(Card card);
    }
}