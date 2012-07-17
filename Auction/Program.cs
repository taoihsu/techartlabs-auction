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
            auc.AddBuyer(new Buyer("blogin1", "Fred", "Durst"));
            auc.AddBuyer(new Buyer("blogin2", "Samuel", "Rivers"));
            auc.AddBuyer(new Buyer("blogin3", "John", "Otto"));
            auc.AddBuyer(new Buyer("blogin4", "Wesley", "Borland"));
            auc.AddSeller(new Seller("slogin1", "Sid", "Wilson"));
            auc.AddSeller(new Seller("slogin2", "Jonas", "Jordison"));
            auc.AddSeller(new Seller("slogin3", "Chris", "Fehn"));
            auc.AddSeller(new Seller("slogin4", "James", "Root"));
            Series series = new Series("first series");
            Sale sale = new Sale("first sale", new Lot("first lot", "amazing lot!", null), 100, new Seller("login1", "Ivan", "Petrushkin"), new TimeSpan(100000), new Category("elite"));
            series.AddSale(sale);
            auc.AddSeries(series);

            sale.MakeBid(new Bid(120, auc.GetBuyerByLogin("blogin1")));
            sale.MakeBid(new Bid(125, auc.GetBuyerByLogin("blogin2")));
            sale.MakeBid(new Bid(127, auc.GetBuyerByLogin("blogin1")));
            sale.MakeBid(new Bid(130, auc.GetBuyerByLogin("blogin3")));

            var login1bids = auc.Bids.Where(b => b.Bidder.Login == "blogin1");
            foreach (var login1Bid in login1bids)
            {
                Console.WriteLine(login1Bid.Value);
            }

        }
    }
}
