using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auction
{
    public class Auction
    {
        public string Name { get; private set; }
        //
        public List<Series> Series { get; private set; }
        public Series this[string saleName]
        {
            get { return Series.First(s => s.Name == saleName); }
        }

        public Auction(string name)
        {
            Name = name;
            Series = new List<Series>();
        }

        public void AddSeries(Series series)
        {
            Series.Add(series);
        }




        public double GetSummaryPrice()
        {
            return Series.Select<Series, double>(s => s.SummaryPrice).Sum();
        }
        public double GetSummaryPriceByCategory(Category category)
        {
            return Series.Select<Series, double>(s => s.GetPriceByCategory(category)).Sum();
        }


        //no implementation
        public List<Buyer> GetActiveBuyers(double percentage)
        {
            return new List<Buyer>();
        }
    }
}
