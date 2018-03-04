namespace MG
{
    public class Dice : IDice
    {
        public virtual DiceResult RollDice()
        {
            var dice1 = Utils.GetRandomNumber(1, 7);
            var dice2 = Utils.GetRandomNumber(1, 7);
            var total = dice1 + dice2;
            var doubleRolled = (dice1 == dice2);

            return new DiceResult(total, doubleRolled);
        }
    }
}
