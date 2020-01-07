using DavidBerry.Framework.ResultType;
using Securities.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.AppInterfaces
{
    public interface ISecurityPriceService
    {



        Result<List<SecurityPrice>> TryGetSecurityPrices(DateTime? date);


        //List<SecurityPrice> GetSecurityPrices(TradeDate date, IEnumerable<String> tickers);

        Result<SecurityPrice> TryGetSecurityPrice(String ticker, DateTime? date);


        //List<SecurityPrice> GetSecurityPrices(String ticker, TradeDate startDate, TradeDate endDate);



    }

}
