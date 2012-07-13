using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auction
{
    public class Series
    {
        public string Name { get; private set; }
        public List<Sale> Sales { get; private set; }
        public double SummaryPrice
        {
            get { return Sales.Select<Sale, double>(l => l.CurrentPrice).Sum(); }
        }

        public Series(string name)
        {
            Name = name;
            Sales = new List<Sale>(); 
        }
        public void AddSale(Sale sale)
        {
            Sales.Add(sale);
        }
        public double GetPrice() { return SummaryPrice; }
        public double GetPriceByCategory(Category category)
        {
            return Sales.Where<Sale>(l => l.Category.Name == category.Name).Select<Sale, double>(l => l.CurrentPrice).Sum();
        }

        public int GetActiveLotCout()
        {
            return Sales.Where<Sale>(l => l.IsActive).Count<Sale>();
        }

        public List<Buyer> GetBuyers()
        {
            List<Buyer> buyers = new List<Buyer>();
            foreach (var lot in Sales)
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
