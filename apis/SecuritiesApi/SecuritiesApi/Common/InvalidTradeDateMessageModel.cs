using DavidBerry.Framework.ApiUtil.Models;
using Securities.Core.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.Common
{
    public class InvalidTradeDateMessageModel : ApiErrorMessageModel
    {


        public InvalidTradeDateMessageModel(InvalidTradeDateError error)
        {
            Message = $"The date {error.Date} is not a valid trade date";
            ErrorCode = "INVALID_TRADE_DATE";
        }


    }
}
