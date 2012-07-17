using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace Auction
{
    public class Auction
    {
        public string Name { get; private set; }
        private List<Series> _series;
        public ReadOnlyCollection<Series> Series {get{return new ReadOnlyCollection<Series>(_series);}}
        public ReadOnlyCollection<Sale> Sales
        {
            get
            {
                var allSales = new List<Sale>();
                foreach (var s in _series)
                {   
                    allSales.AddRange(s.Sales);
                }
                return new ReadOnlyCollection<Sale>(allSales);
            }
        } 
        public ReadOnlyCollection<Bid> Bids
        {
            get
            {
                var allBids = new List<Bid>();
                foreach (var sale in Sales)
                {
                    allBids.AddRange(sale.Bids);
                }
                return new ReadOnlyCollection<Bid>(allBids);
            }
        }
        public Series this[string seriesName]
        {
            get { return GetSeries(seriesName); }
        }

        private List<Seller> _sellers;
        public ReadOnlyCollection<Seller> Sellers { get { return new ReadOnlyCollection<Seller>(_sellers); } }

        private List<Buyer> _buyers;
        public ReadOnlyCollection<Buyer> Buyers { get { return new ReadOnlyCollection<Buyer>(_buyers); } }
        
        public Auction(string name)
        {
            Name = name;
            _series = new List<Series>();
            _sellers = new List<Seller>();
            _buyers = new List<Buyer>();
        }
        public void AddSeries(Series series)
        {
            if (_series.Count(s=>s.Name == series.Name) == 0)
            _series.Add(series);
            else
            {
                throw new DuplicateNameException("Series name is not available");
            }
        }
        public void AddBuyer(Buyer buyer)
        {
            if (_buyers.Count(b => b.Login == buyer.Login) == 0)
                _buyers.Add(buyer);
            else
                throw new DuplicateNameException("login is not available");
        }
        public void AddSeller(Seller seller)
        {
            if (_sellers.Count(s => s.Login == seller.Login) == 0)
                _sellers.Add(seller);
            else
                throw new DuplicateNameException("login is not available");
        }

        public Series GetSeries(string seriesName)
        {
            return _series.First(s => s.Name == seriesName);
        }
        public Buyer GetBuyerByLogin(string buyerLogin)
        {
            return _buyers.Find(b => b.Login == buyerLogin);
        }
        public Buyer GetBuyerByFullName(string firstName, string secondName)
        {
            return _buyers.Find(b => (b.FirstName == firstName) && (b.SecondName == secondName));
        }
        public Seller GetSellerByLogin(string sellerLogin)
        {
            return _sellers.Find(s => s.Login == sellerLogin);
        }
        public Seller GetSellerByFullName(string firstName, string secondName)
        {
            return _sellers.Find(s => (s.FirstName == firstName) && (s.SecondName == secondName));
        }

        public double GetSummaryPrice()
        {
            return _series.Select<Series, double>(s => s.SummaryPrice).Sum();
        }
        public double GetSummaryPriceByCategory(Category category)
        {
            return _series.Select<Series, double>(s => s.GetPriceByCategory(category)).Sum();
        }



        public IEnumerable<Buyer> GetActiveBuyersByBidsCount(int buyersCount)
        {
            List<Buyer> sortedByBidsCount = new List<Buyer>(_buyers); 
            sortedByBidsCount.Sort(BuyersByBidsCountComparer);
            return sortedByBidsCount.Take(buyersCount);
        }
        private int BuyersByBidsCountComparer(Buyer first, Buyer second)
        {
            if (first == null)
            {
                if (second == null)
                { 
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (second == null)
                {
                    return 1;
                }
                else
                {
                    return first.GetBidsCount().CompareTo(second.GetBidsCount());
                }
            }
        }
    }
}
