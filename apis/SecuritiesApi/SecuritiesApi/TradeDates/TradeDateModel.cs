using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.TradeDates
{
    /// <summary>
    /// represents a trading date when US Markets were open and end of day pricing data is available for
    /// </summary>
    public class TradeDateModel
    {

        /// <summary>
        /// Gets the date of this TradeDate
        /// </summary>
        public String TradeDate { get; set; }

        /// <summary>
        /// Checks if this Trade Date was the last trading day of the month
        /// </summary>
        public bool IsMonthEnd { get; set; }

        /// <summary>
        /// Checks if this Trade Date was the last trading day of the quarter
        /// </summary>
        public bool IsQuarterEnd { get; set; }

        /// <summary>
        /// Checks if this Trade Date was the last trading day of the calendar year
        /// </summary>
        public bool IsYearEnd { get; set; }

    }
}
