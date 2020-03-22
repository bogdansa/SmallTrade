using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

//https://4programmers.net/Forum/C_i_.NET/175175-problem_z_watkami_odczyt_i_zapis_do_jednego_pliku
//https://stackoverflow.com/questions/35011656/async-await-in-linq-select <-- to read

//Google: xelement load async

namespace SmallTrade
{
    public class XmlSmallTradeLoaderAsync : ISmallTradeLoaderAsync
    {
        private int _threadNo;

        public Task<IEnumerable<SmallTrade>> LoadTradesAsync(string uri)
        {
            XDocument xmlDocument = XDocument.Load(uri);

            IEnumerable<XElement> tradesList = xmlDocument.Elements("trades");

            IEnumerable<XElement> tradeList = tradesList.Elements("trade");

            return GetSmallTradesAsync(tradeList);
        }

        private async Task<IEnumerable<SmallTrade>> GetSmallTradesAsync(IEnumerable<XElement> tradeList)
        {
            Console.WriteLine("Starting all the tasks");

            IEnumerable<Task<SmallTrade>> smallTrades = tradeList.Select(x => DoNextThread(x, ++_threadNo));

            Console.WriteLine("Waiting for all the tasks to finish\n");

            return await Task.WhenAll(smallTrades.ToList());
        }

        private static Task<SmallTrade> DoNextThread(XElement trade, int threadNo)
        {
            Console.WriteLine($"Before starting the next threadNo={threadNo}");
            Task<SmallTrade> task =  Task.Factory.StartNew(() => CreateOneSmallTrade(trade, threadNo));
            Console.WriteLine($"After  starting the next threadNo={threadNo}\n");

            return task;
        }

        private static SmallTrade CreateOneSmallTrade(XElement trade, int threadNo)
        {
            if (threadNo == 2)
            {
                //Thread.Sleep(2 * 1000);
            }

            var smallTrade = new SmallTrade
            {
                Name = trade.Element("name")?.Value ?? throw new InvalidOperationException(),

                TradeType = (TradeType)Enum.Parse(typeof(TradeType),
                    trade.Element("type")?.Value ?? throw new InvalidOperationException()),

                TradeValue = decimal.Parse(trade.Element("value")?.Value ?? throw new InvalidOperationException()),

                Description = trade.Element("description")?.Value ?? throw new InvalidOperationException()
            };

            Console.WriteLine($"Finishing treadNo {threadNo} - {smallTrade.Name}");

            return smallTrade;
        }
    }
}