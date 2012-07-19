using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Auction
{
    public class Sale
    {
        public string Name { get; private set; }
        public Lot Lot { get; private set; }
        public Category Category { get; private set; }
        private readonly List<Bid> _bids;
        public ReadOnlyCollection<Bid> Bids { get { return new ReadOnlyCollection<Bid>(_bids);} } 
        public Seller Seller { get; private set; }
        public Buyer Buyer { get { return IsActive ? null : LastBidder; } }
        public Buyer LastBidder
        {
            get
            {
                var lastOrDefault = Bids.LastOrDefault();
                if (lastOrDefault != null) return lastOrDefault.Bidder;
                return null;
            }
        }

        public bool CanBuyOut { get; private set; }
        public double BuyOutPrice { get; private set; }

        public double StartPrice { get; private set; }
        public double Increment { get; private set; }
        public double CurrentPrice
        {
            get { return ((Bids.Count == 0) ? StartPrice:  Bids.Last<Bid>().Value); }
        }

        public DateTime StartTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public DateTime FinishTime { get { return StartTime + Duration; } }
        public TimeSpan TimeElapsed { get { return DateTime.Now - StartTime; } }
        public bool IsTimeExpired { get { return DateTime.Now >= FinishTime; } }

        public bool IsActive { get; private set; }
        public bool IsSaled { get { return (Bids.Count > 0) && (IsTimeExpired); } }

        public Sale(string name, Lot lot, Seller seller, double startPrice, double increment, TimeSpan duration, Category category, double buyOutPrice = 0.0)
        {
            Name = name;
            Lot = lot;
            StartTime = DateTime.Now;
            _bids = new List<Bid>();
            StartPrice = startPrice;
            Increment = increment;
            Seller = seller;
            //нужно ли хранить список лотов у людей? - нет
            //Seller.Lots.Add(this);
            if (buyOutPrice > startPrice)
            {
                CanBuyOut = true;
                BuyOutPrice = buyOutPrice;
            }
            else
            { 
                CanBuyOut = false; 
            }
            IsActive = true;
            if (duration < TimeSpan.FromMinutes(1))
                duration = TimeSpan.FromSeconds(1); //исправить! FromMinutes(1)
            Duration = duration;
            Category = category;
        }

        public void MakeBid(Bid bid)
        {
            if (CanBid(bid))
                _bids.Add(bid);
            if (CanBuyOut && (CurrentPrice >= BuyOutPrice))
            {
                //bid.Bidder.LotsBuyed.Add(this);
                IsActive = false;
            }
        }

        private bool CanBid(Bid bid)
        {
            if ((bid.Value - CurrentPrice >= Increment) && (DateTime.Now < FinishTime))
            {
                return true;
            }
            return false;
        }
    }
}
