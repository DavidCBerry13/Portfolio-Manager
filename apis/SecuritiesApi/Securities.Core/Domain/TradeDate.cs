using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.Domain
{
    public class TradeDate
    {

        protected internal TradeDate()
        {
            // For Dapper
        }

        protected internal TradeDate(DateTime date, bool isMonthEnd, bool isQuarterEnd, bool isYearEnd)
        {
            Date = date;
            IsMonthEnd = isMonthEnd;
            IsQuarterEnd = isQuarterEnd;
            IsYearEnd = isYearEnd;
        }

        /// <summary>
        /// Gets the date of this TradeDate
        /// </summary>
        public DateTime Date { get; protected internal set; }

        /// <summary>
        /// Checks if this Trade Date was the last trading day of the month
        /// </summary>
        public bool IsMonthEnd { get; protected internal set; }

        /// <summary>
        /// Checks if this Trade Date was the last trading day of the quarter
        /// </summary>
        public bool IsQuarterEnd { get; protected internal set; }

        /// <summary>
        /// Checks if this Trade Date was the last trading day of the calendar year
        /// </summary>
        public bool IsYearEnd { get; protected internal set; }


        /// <summary>
        /// Returns this trade date as a YYYY-MM-DD string
        /// </summary>
        /// <remarks>
        /// There is such a common need to do this it makes sense to create a method to do it.  This method
        /// is separate that the ToString() method because the ToString() method often gets called in debug/diag
        /// scenarios and in those scenarios it makes sense to include the Month/Quarter/Year end flags as well
        /// </remarks>
        /// <returns></returns>
        public string AsString()
        {
            return Date.ToString("yyyy-MM-dd");
        }


        /// <summary>
        /// Gets a string representation of the object.  The returned string contains all of the fields and is suitable for debugging output
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Date:{Date: yyyy-MM-dd}|MonthEnd:{IsMonthEnd}:QuarterEnd:{IsQuarterEnd}:YearEnd:{IsYearEnd} ";
        }


        public override bool Equals(object obj)
        {
            TradeDate other = obj as TradeDate;
            return (other != null) ?
                this.Date.Equals(other.Date)
                : false;
        }


        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

    }
}
