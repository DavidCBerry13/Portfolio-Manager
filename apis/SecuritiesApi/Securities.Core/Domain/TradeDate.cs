using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.Domain
{
    public class TradeDate
    {

        /// <summary>
        /// Gets the date of this TradeDate
        /// </summary>
        public DateTime Date { get; set; }

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
