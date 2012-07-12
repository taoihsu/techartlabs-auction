using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auction
{
    public class Sale
    {
        public Lot Lot { get; private set; }
        public Category Category { get; private set; }
        //отдавать копию ставок?
        public List<Bid> Bids { get; set; }
        public Seller Seller { get; private set; }
        public Buyer Buyer { get { return IsActive ? null : LastBidder; } }
        public Buyer LastBidder { get { return Bids.Last<Bid>().Bidder; } }
        
        public bool CanBuyOut { get; private set; }
        public double BuyOutPrice { get; private set; }

        public double StartPrice { get { return Bids[0].Value; } }
        public double CurrentPrice { get { return Bids.Last<Bid>().Value; } }

        public DateTime StartTime { get { return Bids[0].Time; } }
        public TimeSpan Duration { get; private set; }
        public DateTime FinishTime { get { return StartTime + Duration; } }
        public TimeSpan TimeElapsed { get { return DateTime.Now - StartTime; } }
        public bool IsTimeExpired { get { return DateTime.Now >= FinishTime; } }

        public bool IsActive { get; private set; }
        public bool IsSaled { get { return (Bids.Count > 1) && (IsTimeExpired); } }

        public Sale(double startPrice, Seller seller, TimeSpan duration, Category category, double buyOutPrice = 0.0)
        {
            Bids = new List<Bid>();
            //set startPrice ?
            //время старта в 0-й ставке
            Bids.Add(new Bid(startPrice,null));
            Seller = seller;
            //нужно ли хранить список лотов у людей?
            Seller.Lots.Add(this);
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
            Duration = duration;
            Category = category;
        }

        public void MakeBid(Bid bid)
        {
            //проверка на минимальное увеличение ставки
            if (bid.Value > CurrentPrice)
                Bids.Add(bid);
            if (CanBuyOut && (CurrentPrice >= BuyOutPrice))
            {
                Buyer.LotsBuyed.Add(this);
                IsActive = false;
            }
        }
    }
}
