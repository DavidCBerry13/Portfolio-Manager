using DavidBerry.Framework.Functional;
using Securities.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.DataAccess
{
    public interface ISecurityPriceRepository
    {

        List<SecurityPrice> GetSecurityPrices(TradeDate tradeDate);


        List<SecurityPrice> GetSecurityPrices(TradeDate tradeDate, IEnumerable<String> tickers);

        Maybe<SecurityPrice> GetSecurityPrice(String ticker, TradeDate tradeDate);


        //List<SecurityPrice> GetSecurityPrices(String ticker, TradeDate startDate, TradeDate endDate);


    }
}
