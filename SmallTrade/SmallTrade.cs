namespace SmallTrade
{
    public enum TradeType
    {
        // ReSharper disable once UnusedMember.Global
        General,
        // ReSharper disable once UnusedMember.Global
        Long,
        // ReSharper disable once UnusedMember.Global
        Short
    }

    public class SmallTrade
    {
        public string Name { get; set; }

        public TradeType? TradeType { get; set; }

        public decimal? TradeValue { get; set; }

        public string Description { get; set; }
    }
}
