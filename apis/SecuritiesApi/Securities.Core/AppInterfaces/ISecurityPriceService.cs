using DavidBerry.Framework.Functional;
using Securities.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.AppInterfaces
{
    public interface ISecurityPriceService
    {


        /// <summary>
        /// Gets security prices for all the securities on the specified date.  If not date is provided, then the latest trade date is used
        /// </summary>
        /// <param name="date">A DateTime of the date to get prices for.  If this value is null, then the latest trade date is used</param>
        /// <returns>A List of SecurityPrice objects</returns>
        Result<List<SecurityPrice>> GetSecurityPrices(DateTime? date);


        Result<List<SecurityPrice>> GetSecurityPrices(DateTime? date, IEnumerable<String> tickers);

        Result<SecurityPrice> GetSecurityPrice(String ticker, DateTime? date);


        /// <summary>
        /// Get prices for a single ticker (security) for the dates specified
        /// </summary>
        /// <remarks>
        /// The provided dates do not have to be trade dates but will instead be used as a range to define
        /// what prices to return.  This allows you to do things like say I want the prices for a calendar year
        /// (1/1 - 12/31) without worrying that 1/1 or 12/31 might be a holiday, weekend, etc.
        /// </remarks>
        /// <param name="ticker">A string of the ticker for the security</param>
        /// <param name="startDate">A DateTime of the first date to get prices for</param>
        /// <param name="endDate">A DateTIme of the last day to get prices for</param>
        /// <returns>A List of SecurityPrice objects</returns>
        Result<List<SecurityPrice>> GetSecurityPrices(String ticker, DateTime? startDate, DateTime? endDate);



    }

}
