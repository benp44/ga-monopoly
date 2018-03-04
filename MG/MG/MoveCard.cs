namespace MG
{
    public class MoveCard : Card
    {
        public Square MoveToSquare { get; private set; }

        public MoveCard(Square moveToSquare)
        {
            MoveToSquare = moveToSquare;
        }
    }
}
