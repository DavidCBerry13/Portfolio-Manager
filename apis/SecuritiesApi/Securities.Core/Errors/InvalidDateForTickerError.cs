using DavidBerry.Framework.Functional;
using Securities.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Securities.Core.Errors
{
    public class InvalidDateForTickerError : Error
    {

        public InvalidDateForTickerError(TradeDate tradeDate, Security security)
            : base($"The ticker {security.Ticker} does not have any data for {tradeDate.Date: yyyy-MM-dd}.  Data for {security.Ticker} is available from {security.FirstTradeDate:yyyy-MM-dd} to {security.LastTradeDate:yyyy-MM-dd}")
        {
            TradeDate = tradeDate;
            Securities = (new List<Security>() { security }).AsReadOnly();
        }

        public InvalidDateForTickerError(TradeDate tradeDate, IEnumerable<Security> securities)
            : base(String.Format("One or more tickers exist but do not have data for the date {0: yyyy-MM-dd}\n{1}",
                tradeDate.Date,
                CreateSecurityListMessageString(securities)))
        {
            TradeDate = tradeDate;
            Securities = securities.ToList().AsReadOnly();
        }

        protected internal static String CreateSecurityListMessageString(IEnumerable<Security> securities)
        {
            return String.Join("\n", securities.Select(s => $"{s.Ticker} ({s.FirstTradeDate.Date.ToString("yyyy-MM-dd")} - {s.LastTradeDate.Date.ToString("yyyy-MM-dd")})"));
        }



        public TradeDate TradeDate { get; private set; }

        public IReadOnlyList<Security> Securities { get; private set; }




    }
}
