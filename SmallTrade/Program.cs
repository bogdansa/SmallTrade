using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallTrade
{
    internal class Program
    {
        private const string XmlSourceData = "SmallTrades.xml";

        private static void Main()
        {
            var xmlSmallTradeLoader = new XmlSmallTradeLoader();

            List<SmallTrade> smallTrades = xmlSmallTradeLoader.LoadTrades(XmlSourceData).ToList();


            var xmlSmallTradeLoaderAsync = new XmlSmallTradeLoaderAsync();

            List<Task<SmallTrade>> smallTradesAsync = xmlSmallTradeLoaderAsync.LoadTradesAsync(XmlSourceData).ToList();

            Console.WriteLine("Press 'Enter' key to close the program");
            Console.Read();
        }
    }
}
