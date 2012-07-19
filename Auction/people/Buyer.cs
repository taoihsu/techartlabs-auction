using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auction
{
    public class Buyer : Person
    {
        //public List<Bid> Bids { get; private set; }
        //public List<Sale> LotsBuyed { get; private set; }

        public Buyer(string login, string firstName, string secondName)
            : base(login, firstName, secondName)
        {
        //    Bids = new List<Bid>();
        //    LotsBuyed = new List<Sale>();
        }

        //public int GetPurchaseCount()
        //{
        //    return LotsBuyed.Count;
        //}

        //public int GetBidsCount()
        //{
        //    return Bids.Count;
        //}

        //public double GetAveragePrice()
        //{
        //    double priceSum = LotsBuyed.Sum(sale => sale.CurrentPrice);
        //    return priceSum/GetPurchaseCount();
        //}
    }
}
