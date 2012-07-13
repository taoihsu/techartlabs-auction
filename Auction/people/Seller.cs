using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auction
{
    public class Seller: Person
    {
        public List<Sale> Lots { get; private set; }

        internal Seller(string login, string firstName, string secondName)
            : base(login, firstName, secondName)
        {
            Lots = new List<Sale>();
        }
    }
}
