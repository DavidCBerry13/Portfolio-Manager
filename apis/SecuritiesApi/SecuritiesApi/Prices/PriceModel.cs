using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.Prices
{
    public class PriceModel
    {

        public String Ticker { get; set; }


        public String Name { get; set; }

        public String SecurityType { get; set; }

        public String TradeDate { get; set; }

        public decimal? OpeningPrice { get; set; }

        public decimal? ClosingPrice { get; set; }

        public decimal? DailyHigh { get; set; }

        public decimal? DailyLow { get; set; }

        public long? Volume { get; set; }

        public decimal? Change { get; set; }

        public decimal? ChangePercent { get; set; }

    }
}
