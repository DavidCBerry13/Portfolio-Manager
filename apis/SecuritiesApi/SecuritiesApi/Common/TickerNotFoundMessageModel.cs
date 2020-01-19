using DavidBerry.Framework.ApiUtil.Models;
using Securities.Core.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.Common
{
    public class TickerNotFoundMessageModel : ApiErrorMessageModel
    {

        public TickerNotFoundMessageModel(TickerNotFoundError error)
        {
            this.Message = error.Message;
            this.ErrorCode = "TICKER_NOT_FOUND";
            this.Tickers = error.InvalidTickers.ToList();
        }



        public List<string> Tickers { get; set; }

    }
}
