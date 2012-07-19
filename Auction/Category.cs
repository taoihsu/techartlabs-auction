namespace Auction
{
    public class Category
    {
        public string Name { get; private set; }
        public double Value { get; private set; }

        public Category(string name, double value = 0)
        {
            Name = name;
            Value = value;
        }


    }
}
