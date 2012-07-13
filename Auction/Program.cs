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
            Sale sale = new Sale("first sale",new Lot("first lot","amazing lot!",null), 100,new Seller("Ivan","Petrushkin"),new TimeSpan(100000),new Category("elite"));
            sale.Bids.Add(new Bid(102,new Buyer("Petya","---")));

        }
    }
}
