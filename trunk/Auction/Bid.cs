using System;

namespace Auction
{
    public class Bid
    {
        public double Value { get; private set; }
        public Buyer Bidder { get; private set; }
        public DateTime Time { get; private set; }

        public Bid(double value, Buyer bidder)
        {
            Value = value;
            Bidder = bidder;
            Time = DateTime.Now;
        }
    }
}
