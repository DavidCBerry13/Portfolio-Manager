using DavidBerry.Framework.ApiUtil.Models;
using Securities.Core.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.Common
{
    public class TickerNotFoundMessageModel : ApiMessageModel
    {

        public TickerNotFoundMessageModel(TickerNotFoundError error)
        {
            this.Message = error.Message;
            this.ErrorCode = "INVALID_TICKER";
            this.Tickers = error.InvalidTickers.ToList();
        }


        public string ErrorCode { get; set; }


        public List<string> Tickers { get; set; }

    }
}
