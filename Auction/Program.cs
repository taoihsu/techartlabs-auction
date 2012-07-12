using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auction
{
    class Program
    {
        static void Main(string[] args)
        {
            Auction auc = new Auction("test auction");
            Sale sale = new Sale(100,new Seller(), new TimeSpan(100000),new Category("category1"),10000 );
            //sale.Bids.Add(new Bid(102,new Buyer()));
        }
    }
}
