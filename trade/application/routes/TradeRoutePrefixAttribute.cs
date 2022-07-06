using System.Web.Http;

namespace trade.application.routes
{
    public class TradeRoutePrefixAttribute : RoutePrefixAttribute
    {
        public TradeRoutePrefixAttribute() : base("api") { }
        public TradeRoutePrefixAttribute(string prefix) : base("api/" + prefix) { }
    }
}