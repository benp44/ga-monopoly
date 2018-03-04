namespace MG
{
    public class MoneyCard : Card
    {
        public int Sum { get; private set; }

        public MoneyCard(int sum)
        {
            Sum = sum;
        }
    }
}
