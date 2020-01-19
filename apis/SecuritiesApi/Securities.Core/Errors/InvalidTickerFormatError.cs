using DavidBerry.Framework.Functional;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.Errors
{
    public class InvalidTickerFormatError : Error
    {
        public InvalidTickerFormatError(String invalidTicker)
            : base($"The supplied ticker '{invalidTicker}' is in invalid (invalid format)")
        {
            InvalidTicker = invalidTicker;
        }

        public String InvalidTicker { get; set; }

    }
}
