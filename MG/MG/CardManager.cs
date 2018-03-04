using System;
using System.Collections.Generic;

namespace MG
{
    public class CardManager : ICardManager
    {
        public Queue<Card> ChanceDeck { get; private set; }
        public Queue<Card> CommunityChestDeck { get; private set; }
        public Board Board { get; private set; }

        public CardManager(Board board)
        {
            Board = board;

            ChanceDeck = new Queue<Card>();

            ChanceDeck.Enqueue(new MoveCard(Board.Go));
            ChanceDeck.Enqueue(new MoveCard(Board.TrafalgarSquare));
            ChanceDeck.Enqueue(new MoveCard(Board.PallMall));
            ChanceDeck.Enqueue(new SpecialCard(SpecialCardType.NextUtility));
            ChanceDeck.Enqueue(new SpecialCard(SpecialCardType.NextStation));
            ChanceDeck.Enqueue(new MoneyCard(50));
            ChanceDeck.Enqueue(new SpecialCard(SpecialCardType.GetOutOfJailFreeChance));
            ChanceDeck.Enqueue(new SpecialCard(SpecialCardType.BackThree));
            ChanceDeck.Enqueue(new SpecialCard(SpecialCardType.GoToJail));
            ChanceDeck.Enqueue(new SpecialCard(SpecialCardType.RepairsChance));
            ChanceDeck.Enqueue(new MoneyCard(-15));
            ChanceDeck.Enqueue(new MoveCard(Board.KingsCrossStation));
            ChanceDeck.Enqueue(new MoveCard(Board.Mayfair));
            ChanceDeck.Enqueue(new SpecialCard(SpecialCardType.GiveEachPlayerFifty));
            ChanceDeck.Enqueue(new MoneyCard(150));
            ChanceDeck.Enqueue(new MoneyCard(100));

            ChanceDeck.Shuffle();

            CommunityChestDeck = new Queue<Card>();
            CommunityChestDeck.Enqueue(new MoveCard(Board.Go));
            CommunityChestDeck.Enqueue(new MoneyCard(200));
            CommunityChestDeck.Enqueue(new MoneyCard(-50));
            CommunityChestDeck.Enqueue(new MoneyCard(50));
            CommunityChestDeck.Enqueue(new SpecialCard(SpecialCardType.GetOutOfJailFreeCommunityChest));
            CommunityChestDeck.Enqueue(new SpecialCard(SpecialCardType.GoToJail));
            CommunityChestDeck.Enqueue(new SpecialCard(SpecialCardType.ReceiveFiftyFromEachPlayer));
            CommunityChestDeck.Enqueue(new MoneyCard(100));
            CommunityChestDeck.Enqueue(new MoneyCard(20));
            CommunityChestDeck.Enqueue(new MoneyCard(10));
            CommunityChestDeck.Enqueue(new MoneyCard(100));
            CommunityChestDeck.Enqueue(new MoneyCard(-100));
            CommunityChestDeck.Enqueue(new MoneyCard(-150));
            CommunityChestDeck.Enqueue(new MoneyCard(25));
            CommunityChestDeck.Enqueue(new SpecialCard(SpecialCardType.RepairsCommunityChest));
            CommunityChestDeck.Enqueue(new MoneyCard(10));
            CommunityChestDeck.Enqueue(new MoneyCard(100));

            CommunityChestDeck.Shuffle();
        }

        public Card GetNextCommunityChest()
        {
            var card = CommunityChestDeck.Dequeue();

            if (card is SpecialCard) 
            {
                if (((SpecialCard)card).CardType != SpecialCardType.GetOutOfJailFreeCommunityChest)
                {
                    CommunityChestDeck.Enqueue(card);
                }
            }
            else
            {
                CommunityChestDeck.Enqueue(card);
            }

            return card;
        }

        public Card GetNextChance()
        {
            var card = ChanceDeck.Dequeue();

            if (card is SpecialCard)
            {
                if (((SpecialCard)card).CardType != SpecialCardType.GetOutOfJailFreeChance)
                {
                    ChanceDeck.Enqueue(card);
                }
            }
            else
            {
                ChanceDeck.Enqueue(card);
            }

            return card;
        }

        public void ReturnGetOutOfJailFreeCard(Card card)
        {
            var sCard = (SpecialCard)card;

            if (sCard.CardType == SpecialCardType.GetOutOfJailFreeChance)
            {
                ChanceDeck.Enqueue(card);
            }
            else if (sCard.CardType == SpecialCardType.GetOutOfJailFreeCommunityChest)
            {
                CommunityChestDeck.Enqueue(card);
            }
            else
            {
                throw new ApplicationException("Unknown card return");
            }
        }
    }
}
