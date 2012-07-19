using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace Auction
{
    public class Auction
    {
        public string Name { get; private set; }
        private readonly List<Series> _series;
        public ReadOnlyCollection<Series> Series
        {
            get { return new ReadOnlyCollection<Series>(_series); }
        }
        //private readonly List<Sale> _sales;
        //public ReadOnlyCollection<Sale> Sales
        //{
        //    get { return new ReadOnlyCollection<Sale>(_sales); }
        //}
        //private readonly List<Bid> _bids;
        //public ReadOnlyCollection<Bid> Bids
        //{
        //    get { return new ReadOnlyCollection<Bid>(_bids); }
        //}

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

        private readonly List<Seller> _sellers;
        public ReadOnlyCollection<Seller> Sellers { get { return new ReadOnlyCollection<Seller>(_sellers); } }

        private readonly List<Buyer> _buyers;
        public ReadOnlyCollection<Buyer> Buyers { get { return new ReadOnlyCollection<Buyer>(_buyers); } }
        public AuctionSettings Settings;

        public Auction(string name)
        {
            Name = name;
            _series = new List<Series>();
            _sellers = new List<Seller>();
            _buyers = new List<Buyer>();
            Settings = new AuctionSettings();
        }
        public void AddSeries(Series series)
        {
            if (_series.Count(s=>s.Name == series.Name) == 0)
            {
                _series.Add(series);
                series.Settings = Settings;
            }
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
            return _series.Select(s => s.SummaryPrice).Sum();
        }
        public double GetSummaryPriceByCategory(Category category)
        {
            return _series.Select(s => s.GetPriceByCategory(category)).Sum();
        }

        public void MakeBid(Sale sale, Bid bid)
        {
            if (!Sales.Contains(sale)) return;
            if (GetUserBids(bid.Bidder).Sum(b=>b.Value) >= sale.Category.Restriction)
            {
                sale.RegisterBid(bid);
            }
        }

        public IEnumerable<Bid> GetUserBids(Buyer buyer)
        {
            return Bids.Where(b => b.Bidder == buyer);
        }

        public IEnumerable<Sale> GetUserPurchase(Buyer buyer)
        {
            return Sales.Where(s => s.IsSaled).Where(s => s.Buyer == buyer);
        }

        public IEnumerable<Buyer> GetActiveBuyersByBidsCount(int buyersCount)
        {
            var buyersByBidCount = new Dictionary<Buyer, int>();
            foreach (var bid in Bids)
            {
                if (buyersByBidCount.ContainsKey(bid.Bidder))
                {
                    buyersByBidCount[bid.Bidder]++;
                }
                else
                {
                    buyersByBidCount.Add(bid.Bidder, 0);
                }
            }
            var result = buyersByBidCount.OrderBy(b => b.Value).Select(source => source.Key);
            return result;
        }
    }
}
