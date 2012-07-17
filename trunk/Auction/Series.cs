using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Auction
{
    public class Series
    {
        public string Name { get; private set; }
        private List<Sale> _sales;
        public ReadOnlyCollection<Sale> Sales { get { return new ReadOnlyCollection<Sale>(_sales); } }
        public ReadOnlyCollection<Bid> Bids
        {
            get
            {
                var allBids = new List<Bid>();
                foreach (var sale in _sales)
                {
                    allBids.AddRange(sale.Bids);
                }
                return new ReadOnlyCollection<Bid>(allBids);
            }
        }

        public double SummaryPrice
        {
            get { return _sales.Select<Sale, double>(l => l.CurrentPrice).Sum(); }
        }

        public Series(string name)
        {
            Name = name;
            _sales = new List<Sale>(); 
        }
        public void AddSale(Sale sale)
        {
            _sales.Add(sale);
        }
        public double GetPrice() { return SummaryPrice; }
        public double GetPriceByCategory(Category category)
        {
            return _sales.Where<Sale>(l => l.Category.Name == category.Name).Select<Sale, double>(l => l.CurrentPrice).Sum();
        }

        public int GetActiveLotCout()
        {
            return _sales.Count(l => l.IsActive);
        }

        public List<Buyer> GetBuyers()
        {
            List<Buyer> buyers = new List<Buyer>();
            foreach (var lot in _sales)
            {
                if (!buyers.Contains<Buyer>(lot.Buyer))
                    buyers.Add(lot.Buyer);
            }
            return buyers;
        }

        /*
        public List<Buyer> GetActiveBuyers(double percentage)
        {
            List<Buyer> activeBuyers = new List<Buyer>();
            foreach (var buyer in GetBuyers())
            {
                activeBuyers.Add(buyer);
            }

            int activeBuyersCount = (int)(activeBuyers.Count * percentage / 100);
            List<Buyer> activeBuyersByPercentage = new List<Buyer>();
        }
        */

    }
}
