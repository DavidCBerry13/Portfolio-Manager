using DavidBerry.Framework.ApiUtil.Models;
using Securities.Core.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.Common
{
    public class InvalidTickerFormatMessageModel : ApiErrorMessageModel
    {

        public InvalidTickerFormatMessageModel(InvalidTickerFormatError error)
        {
            Message = $"The value {error.InvalidTicker} is an invalid format for a ticker";
            ErrorCode = "INVALID_TICKER";
        }




    }
}
