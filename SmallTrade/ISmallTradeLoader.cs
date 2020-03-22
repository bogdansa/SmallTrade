using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmallTrade
{
    public interface ISmallTradeLoader
    {
        IEnumerable<SmallTrade> LoadTrades(string uri);
    }

    public interface ISmallTradeLoaderAsync
    {
        Task<IEnumerable<SmallTrade>> LoadTradesAsync(string uri);
    }
}
