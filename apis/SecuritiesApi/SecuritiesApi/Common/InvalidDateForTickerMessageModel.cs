using DavidBerry.Framework.ApiUtil.Models;
using Securities.Core.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.Common
{
    public class InvalidDateForTickerMessageModel : ApiErrorMessageModel
    {

        public InvalidDateForTickerMessageModel(InvalidDateForTickerError error)
        {
            this.Message = error.Message;
            this.ErrorCode = "INVALID_DATES_FOR_TICKER";
            TradeDate = error.TradeDate.Date.ToString("yyyy-MM-dd");
            Tickers = error.Securities.Select(s => new InvalidTickerDateModel()
            {
                Ticker = s.Ticker,
                DataStartDate = s.FirstTradeDate.ToString("yyyy-MM-dd"),
                DateEndDate = s.LastTradeDate.ToString("yyyy-MM-dd")
            }
            ).ToList();
        }


        public String TradeDate { get; set; }

        public List<InvalidTickerDateModel> Tickers { get; set; }




        public class InvalidTickerDateModel
        {
            public String Ticker { get; set; }

            public String DataStartDate { get; set; }

            public String DateEndDate { get; set; }
        }
    }
}
