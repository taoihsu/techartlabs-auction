﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auction
{
    public class Buyer: Person
    {
        public List<Bid> Bids { get; private set; }
        public List<Sale> LotsBuyed { get; private set; }

        public Buyer(string firstName, string secondName)
            : base(firstName, secondName)
        {
            Bids = new List<Bid>();
            LotsBuyed = new List<Sale>();
        }

        
    }
}