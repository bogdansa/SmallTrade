using System.Collections.Generic;

namespace SmallTrade
{
    public interface ISmallTradeLoader
    {
        IEnumerable<SmallTrade> LoadTrades(string uri);
    }
}
