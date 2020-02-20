using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SmallTrade
{
    public class XmlSmallTradeLoader : ISmallTradeLoader
    {
        public IEnumerable<SmallTrade> LoadTrades(string uri)
        {
            var xmlDocument = XDocument.Load(uri);

            var trades = xmlDocument.Elements("trades").Elements("trade");

            var smallTrades = trades.Select(trade => new SmallTrade
            {
                Name = trade.Element("name")?.Value ?? throw new InvalidOperationException(),
                TradeType = (TradeType) Enum.Parse(typeof(TradeType),
                    trade.Element("type")?.Value ?? throw new InvalidOperationException()),
                TradeValue = decimal.Parse(trade.Element("value")?.Value ?? throw new InvalidOperationException()),
                Description = trade.Element("description")?.Value ?? throw new InvalidOperationException()
            });

            return smallTrades;
        }
    }
}
