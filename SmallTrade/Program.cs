using System;
using System.Collections;
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

            Task<IEnumerable<SmallTrade>> smallTradesAsyncTask = xmlSmallTradeLoaderAsync.LoadTradesAsync(XmlSourceData);

            Console.WriteLine("Faking doing extra work in the main thread.");
            Console.WriteLine("Faking doing extra work in the main thread.");
            Console.WriteLine("Faking doing extra work in the main thread.");
            await smallTradesAsyncTask;

            IEnumerable<SmallTrade> smallTradesAsync = smallTradesAsyncTask.Result;

            Console.WriteLine("\nEnd of the program. Press 'Enter' key to close the program.");
            Console.Read();
        }
    }
}.
