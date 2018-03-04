namespace MG
{
    public class DiceResult
    {
        public int Total { get; private set; }
        public bool DoubleRolled { get; private set; }

        public DiceResult(int total, bool doubleRolled)
        {
            Total = total;
            DoubleRolled = doubleRolled;
        }

        public override string ToString()
        {
            var result = Total.ToString();
            if (DoubleRolled)
            {
                result += " (double)";
            }

            return result;
        }
    }

}
