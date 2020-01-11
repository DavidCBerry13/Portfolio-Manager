using DavidBerry.Framework.Functional;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.Errors
{
    public class TickerNotFoundError : Error
    {

        public TickerNotFoundError(string ticker)
            : base($"The ticker {ticker} is not associated with any security")
        {
            this.InvalidTickers = (new List<string>() { ticker }).AsReadOnly();
        }


        public TickerNotFoundError(IEnumerable<string> invalidTickers)
            : base($"The following tickers could not be found ({String.Join(",",invalidTickers)})")
        {
            this.InvalidTickers = invalidTickers.ToList().AsReadOnly();
        }


        public IReadOnlyList<string> InvalidTickers { get; private set; }


    }
}
