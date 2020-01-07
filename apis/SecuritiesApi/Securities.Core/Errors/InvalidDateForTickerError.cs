using DavidBerry.Framework.ResultType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.Errors
{
    public class InvalidDateForTickerError : Error
    {

        public InvalidDateForTickerError(String ticker, DateTime date, DateTime tickerDataStartdate, DateTime tickerDataEndDate)
            : base($"The ticker {ticker} does not have any data for {date: yyyy-MM-dd}.  Data for {ticker} is available from {tickerDataStartdate:yyyy-MM-dd} to {tickerDataEndDate:yyyy-MM-dd}")
        {

        }



    }
}
