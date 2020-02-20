using System.Collections.Generic;
using System.Linq;

namespace SmallTrade
{
    internal class Program
    {
        private const string XmlSourceData = "SmallTrades.xml";

        private static void Main()
        {
            var smallTrades = new List<SmallTrade>();

            var xmlSmallTradeLoader = new XmlSmallTradeLoader();

            smallTrades = xmlSmallTradeLoader.LoadTrades(XmlSourceData).ToList();
        }
    }
}
