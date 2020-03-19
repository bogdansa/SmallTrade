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

        public /*async*/ Task<IEnumerable<Task<SmallTrade>>> LoadTradesAsync(string uri)
        {
            XDocument xmlDocument = XDocument.Load(uri);

            IEnumerable<XElement> tradesList = xmlDocument.Elements("trades");

            IEnumerable<XElement> tradeList = tradesList.Elements("trade");

            Task<IEnumerable<Task<SmallTrade>>> smallTrades = /*await*/ GetSmallTradesAsync(tradeList);

            return smallTrades;
        }

        private async Task<IEnumerable<Task<SmallTrade>>> GetSmallTradesAsync(IEnumerable<XElement> tradeList)
        {
            Console.WriteLine("Starting all the tasks");

            IEnumerable<Task<SmallTrade>> smallTrades = tradeList.Select(x => DoNextThread(x, ++_threadNo));

            List<Task<SmallTrade>> smallTradesAsync = smallTrades.ToList();

            Console.WriteLine("Waiting for all the tasks to finish\n");

            foreach (Task<SmallTrade> task in smallTradesAsync)
            {
                //Console.WriteLine($"Waiting for {task.Result.Name} thread");
                await task;
                Console.WriteLine($"After waiting for {task.Result.Name} thread\n");
            }

            return smallTradesAsync;
        }

        private static Task<SmallTrade> DoNextThread(XElement trade, int threadNo)
        {
            //Count("Second thread A to 500", 500); //when it works it needs to finish first

            Console.WriteLine($"Before starting next threadNo={threadNo}");
            Task<SmallTrade> task =  Task.Factory.StartNew(() => CreateOneSmallTrade(trade, threadNo));
            Console.WriteLine($"After  starting next threadNo={threadNo}\n");

            return task;
        }

        private static SmallTrade CreateOneSmallTrade(XElement trade, int threadNo)
        {

            string tradeName = trade.Element("name")?.Value ?? "";

            //Console.WriteLine($"Starting treadNo {threadNo} - {tradeName}");

            if (threadNo == 2)
            {
                //Thread.Sleep(20 * 1000);
            }

            var smallTrade = new SmallTrade
            {
                Name = tradeName /* ?? throw new InvalidOperationException()*/,

                TradeType = (TradeType)Enum.Parse(typeof(TradeType),
                    trade.Element("type")?.Value ?? throw new InvalidOperationException()),

                TradeValue = decimal.Parse(trade.Element("value")?.Value ?? throw new InvalidOperationException()),

                Description = trade.Element("description")?.Value ?? throw new InvalidOperationException()
            };

            Console.WriteLine($"Finishing treadNo {threadNo} - {tradeName}");

            return smallTrade;
        }
    }
}