using DavidBerry.Framework.ResultType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.Errors
{
    public class TickerNotFoundError : Error
    {

        public TickerNotFoundError(String ticker)
            : base($"The ticker {ticker} is not associated with any security")
        {

        }

    }
}
