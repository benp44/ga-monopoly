using System.Drawing;

namespace MG
{
    public abstract class Square
    {
        abstract public string Name { get; }
        abstract public Color DisplayColor { get; }

        public int Order { get; private set; }

        public Square(int order)
        {
            Order = order;
        }
    }
}
