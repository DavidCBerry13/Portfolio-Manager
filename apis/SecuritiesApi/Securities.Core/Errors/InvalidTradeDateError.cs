using DavidBerry.Framework.ResultType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.Errors
{

    /// <summary>
    /// Represents an error condition of a  trade date that does not exist.  This could be because the trade date
    /// is before or after we have data for or that the trade date is a weekend
    /// </summary>
    public class InvalidTradeDateError: Error
    {

        public InvalidTradeDateError(DateTime date)
            : base($"The date {date:yyyy-MM-dd} is not a valid trade date")
        {

        }

    }
}
