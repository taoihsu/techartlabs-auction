using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auction
{
    public class Seller: Person
    {
        public List<Sale> Lots { get; private set; }

        public Seller(string firstName, string secondName)
            : base(firstName, secondName)
        {
            Lots = new List<Sale>();
        }
    }
}
