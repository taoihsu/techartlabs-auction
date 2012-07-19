using System;
using System.Threading;

namespace Auction
{
    class Program
    {
        static void Main()
        {
            // Создаем аукцион и регистрируем продавцов и покупателей
            var auction = new Auction("test auction");
            auction.AddBuyer(new Buyer("blogin1", "Fred", "Durst"));
            auction.AddBuyer(new Buyer("blogin2", "Samuel", "Rivers"));
            auction.AddBuyer(new Buyer("blogin3", "John", "Otto"));
            auction.AddBuyer(new Buyer("blogin4", "Wesley", "Borland"));
            auction.AddSeller(new Seller("slogin1", "Sid", "Wilson"));
            auction.AddSeller(new Seller("slogin2", "Jonas", "Jordison"));
            auction.AddSeller(new Seller("slogin3", "Chris", "Fehn"));
            auction.AddSeller(new Seller("slogin4", "James", "Root"));

            // Создаем две серии. В примере используется только первая
            var series1 = new Series("first series");
            var series2 = new Series("second series");

            // Добавляем серии в аукцион
            auction.AddSeries(series1);
            Console.WriteLine("series created:\t" + series1.Name);
            auction.AddSeries(series2);
            Console.WriteLine("series created:\t" + series2.Name);
            Console.WriteLine();

            // Создаем лоты
            var lot1 = new Lot("first lot", "amazing lot!", null);
            var lot2 = new Lot("second lot", "complex lot", null);
            var lot3 = new Lot("third lot", "Green Book", null);
            var lot4 = new Lot("fourth lot", "Red Book", null);
            var lot5 = new Lot("fifth lot", "Black Book", null, lot1, lot2, lot3, lot4);

            // Создаем продажу длительностью 5 секунд
            var sale1 = new Sale("first sale", lot5, auction.GetSellerByLogin("slogin1"), 
                100, 1, TimeSpan.FromSeconds(5), new Category("elite"));


            // Добавляем продажу
            series1.AddSale(sale1);
            // Ожидаем, пока продажа закончится и сообщаем об этом
            var waitForFinish = new Thread(FinishMessage);
            waitForFinish.Start(sale1);
            Console.WriteLine("sale created:\t" + sale1.Name +"\tduration (seconds):" + sale1.Duration.TotalSeconds + "\t{series = " + series1.Name+"}");

            // Создаем ставки
            var bid1 = new Bid(120, auction.GetBuyerByLogin("blogin1"));
                var bid12 = new Bid(121, auction.GetBuyerByLogin("blogin1")); //вторая ставка одним покупателем. не должна пройти
            var bid2 = new Bid( 125, auction.GetBuyerByLogin("blogin2"));
            var bid3 = new Bid( 127, auction.GetBuyerByLogin("blogin1"));
            var bid4 = new Bid( 130, auction.GetBuyerByLogin("blogin3"));
            var bid5 = new Bid( 131, auction.GetBuyerByLogin("blogin1"));

            // Делаем ставки через 0.5 секунды и выводим последнюю принятую ставку
            auction.MakeBid(sale1, bid1);
            auction.MakeBid(sale1, bid12); // не принимается (последняя ставка остается 120)
            Console.WriteLine("sale #" + sale1.Number + "\t\"" + sale1.Name + "\"\tlastbidder:\t" + (sale1.LastBidder==null?"none":sale1.LastBidder.FullName) + "\t val:" + sale1.CurrentPrice);
            Thread.Sleep(500);
            auction.MakeBid(sale1, bid2);
            Console.WriteLine("sale #" + sale1.Number + "\t\"" + sale1.Name + "\"\tlastbidder:\t" + (sale1.LastBidder == null ? "none" : sale1.LastBidder.FullName) + "\t val:" + sale1.CurrentPrice);
            Thread.Sleep(500);
            auction.MakeBid(sale1, bid3);
            Console.WriteLine("sale #" + sale1.Number + "\t\"" + sale1.Name + "\"\tlastbidder:\t" + (sale1.LastBidder == null ? "none" : sale1.LastBidder.FullName) + "\t val:" + sale1.CurrentPrice);
            Thread.Sleep(500);
            auction.MakeBid(sale1, bid4);
            Console.WriteLine("sale #" + sale1.Number + "\t\"" + sale1.Name + "\"\tlastbidder:\t" + (sale1.LastBidder == null ? "none" : sale1.LastBidder.FullName) + "\t val:" + sale1.CurrentPrice);
            Thread.Sleep(500);
            auction.MakeBid(sale1, bid5);
            Console.WriteLine("sale #" + sale1.Number + "\t\"" + sale1.Name + "\"\tlastbidder:\t" + (sale1.LastBidder == null ? "none" : sale1.LastBidder.FullName) + "\t val:" + sale1.CurrentPrice);
            Thread.Sleep(500);


            Console.ReadKey();
        }

        // Выводит сообщение об окончании торгов при из завершении
        public static void FinishMessage(object saleArg)
        {
            var sale = (Sale) saleArg;
            var timeout = sale.FinishTime - DateTime.Now +TimeSpan.FromSeconds(0.001);
            Thread.Sleep(timeout);
            Console.WriteLine("sale #"+sale.Number+" finished!\tWinnwer:\t"+(sale.Buyer==null?"none":sale.Buyer.FullName));
        }
    }
}
