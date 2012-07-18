using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Auction
{
    class Program
    {
        static void Main(string[] args)
        {
            Auction auc = new Auction("test auction");
            auc.AddBuyer(new Buyer("blogin1", "Fred", "Durst"));
            auc.AddBuyer(new Buyer("blogin2", "Samuel", "Rivers"));
            auc.AddBuyer(new Buyer("blogin3", "John", "Otto"));
            auc.AddBuyer(new Buyer("blogin4", "Wesley", "Borland"));
            auc.AddSeller(new Seller("slogin1", "Sid", "Wilson"));
            auc.AddSeller(new Seller("slogin2", "Jonas", "Jordison"));
            auc.AddSeller(new Seller("slogin3", "Chris", "Fehn"));
            auc.AddSeller(new Seller("slogin4", "James", "Root"));
            Series series = new Series("first series");

            var lot1 = new Lot("first lot", "amazing lot!", null);
            var lot2 = new Lot("second lot", "complex lot", null, lot1);
            Sale sale = new Sale("first sale", lot2, new Seller("login1", "Ivan", "Petrushkin"), 100, 1, new TimeSpan(1), new Category("elite"));
            series.AddSale(sale);
            auc.AddSeries(series);

            sale.MakeBid(new Bid(120, auc.GetBuyerByLogin("blogin1")));
            sale.MakeBid(new Bid(125, auc.GetBuyerByLogin("blogin2")));
            sale.MakeBid(new Bid(127, auc.GetBuyerByLogin("blogin1")));
            sale.MakeBid(new Bid(130, auc.GetBuyerByLogin("blogin3")));
            Thread.Sleep(2000);



            var login1bids = auc.Bids.Where(b => b.Bidder.Login == "blogin1");
            var salesbuyed = auc.Sales.Where(s => s.IsSaled == true);

            foreach (var sale1 in salesbuyed)
            {
                Console.WriteLine(sale1.Name);
            }
            //comment!
        }
    }
}
