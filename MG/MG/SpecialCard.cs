namespace MG
{
    public enum SpecialCardType
    {
        NextUtility, // pay 10x regardless
        NextStation, // pay double!
        BackThree,
        GoToJail,
        GetOutOfJailFreeChance,
        GetOutOfJailFreeCommunityChest,
        RepairsChance,
        GiveEachPlayerFifty,
        ReceiveFiftyFromEachPlayer,
        RepairsCommunityChest,
    }

    public class SpecialCard : Card
    {
        public SpecialCardType CardType { get; private set; }

        public SpecialCard (SpecialCardType specialMoveType)
        {
            CardType = specialMoveType;
        }
    }
}
