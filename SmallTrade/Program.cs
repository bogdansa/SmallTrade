using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallTrade
{
    internal class Program
    {
        private const string XmlSourceData = "SmallTrades.xml";

        private static async Task Main()
        {
            var xmlSmallTradeLoader = new XmlSmallTradeLoader();

            List<SmallTrade> smallTrades = xmlSmallTradeLoader.LoadTrades(XmlSourceData).ToList();


            var xmlSmallTradeLoaderAsync = new XmlSmallTradeLoaderAsync();

            Task<IEnumerable<Task<SmallTrade>>> smallTradesAsyncTask = xmlSmallTradeLoaderAsync.LoadTradesAsync(XmlSourceData);
            await smallTradesAsyncTask;
            IEnumerable<Task<SmallTrade>> smallTradesAsync = smallTradesAsyncTask.Result;

            Console.WriteLine("Press 'Enter' key to close the program");
            Console.Read();
        }
    }
}
