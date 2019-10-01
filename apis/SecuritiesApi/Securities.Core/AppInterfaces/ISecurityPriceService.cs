using Framework.OperationResults;
using Securities.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.AppInterfaces
{
    public interface ISecurityPriceService
    {



        Result<SecurityPriceErrorCode> TryGetSecurityPrices(DateTime? date, out List<SecurityPrice> securityPrices);


        //List<SecurityPrice> GetSecurityPrices(TradeDate date, IEnumerable<String> tickers);

        Result<SecurityPriceErrorCode> TryGetSecurityPrice(String ticker, DateTime? date, out SecurityPrice securityPrice);


        //List<SecurityPrice> GetSecurityPrices(String ticker, TradeDate startDate, TradeDate endDate);



    }


    public class SecurityPriceErrorCode : ErrorCode
    {
        public static readonly SecurityPriceErrorCode TRADE_DATE_NOT_VALID = ErrorCode.New<SecurityPriceErrorCode>(1);
        public static readonly SecurityPriceErrorCode TICKER_DOES_NOT_EXIST = ErrorCode.New<SecurityPriceErrorCode>(2);
        public static readonly SecurityPriceErrorCode NO_DATA_FOR_TICKER_ON_TRADE_DATE = ErrorCode.New<SecurityPriceErrorCode>(3);
    }

}
