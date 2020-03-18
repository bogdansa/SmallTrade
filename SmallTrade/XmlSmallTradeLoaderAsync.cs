using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading;
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

        public IEnumerable<Task<SmallTrade>> LoadTradesAsync(string uri)
        {
            XDocument xmlDocument = XDocument.Load(uri);

            IEnumerable<XElement> tradesList = xmlDocument.Elements("trades");

            IEnumerable<XElement> tradeList = tradesList.Elements("trade");

            IEnumerable<Task<SmallTrade>> smallTrades = GetSmallTradesAsync(tradeList);

            return smallTrades;
        }

        private IEnumerable<Task<SmallTrade>> GetSmallTradesAsync(IEnumerable<XElement> tradeList)
        {
            return tradeList.Select(async x => await DoNextThread(x, ++_threadNo));
        }

        private static Task<SmallTrade> DoNextThread(XElement trade, int threadNo)
        {
            //Count("Second thread A to 500", 500); //it it works it needs to finish first

            Console.WriteLine($"Before starting next threadNo={threadNo}");
            Task<SmallTrade> task =  Task.Factory.StartNew(() => CreateOneSmallTrade(trade, threadNo));
            Console.WriteLine($"After  starting next threadNo={threadNo}");

            return task;
        }

        private static SmallTrade CreateOneSmallTrade(XElement trade, int threadNo = 1)
        {
            Console.WriteLine($"Inside treadNo {threadNo}");
            if (threadNo == 1)
            {
                //Thread.Sleep(1000); 
            }

            return new SmallTrade
            {
                Name = trade.Element("name")?.Value ?? throw new InvalidOperationException(),

                TradeType = (TradeType)Enum.Parse(typeof(TradeType),
                    trade.Element("type")?.Value ?? throw new InvalidOperationException()),

                TradeValue = decimal.Parse(trade.Element("value")?.Value ?? throw new InvalidOperationException()),

                Description = trade.Element("description")?.Value ?? throw new InvalidOperationException()
            };
        }
    }
}