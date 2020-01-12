using System;
using System.Collections.Generic;
using System.Text;
using DavidBerry.Framework.Functional;
using DavidBerry.Framework.Util;
using Securities.Core.AppInterfaces;
using Securities.Core.DataAccess;
using Securities.Core.Domain;
using Securities.Core.Errors;

namespace Securities.Core.AppServices
{
    public class TradeDateService : ITradeDateService
    {

        public TradeDateService(ITradeDateRepository repository)
        {
            _tradeDateRepository = repository;
        }

        private readonly ITradeDateRepository _tradeDateRepository;

        public Result<TradeDate> GetLatestTradeDate()
        {
            var tradeDate = _tradeDateRepository.GetLatestTradeDate();
            return tradeDate.Eval<Result<TradeDate>>(
                d => Result.Success<TradeDate>(d),
                () => Result.Failure<TradeDate>(new ApplicationError("Unable to get latest trade date.  Validate trade dates are loaded into the system")));
        }

        public Result<TradeDate> GetTradeDate(DateTime date)
        {
            var tradeDate = _tradeDateRepository.GetTradeDate(date.Date);
            return tradeDate.Eval<Result<TradeDate>>(
                (d) => Result.Success<TradeDate>(d),
                () => Result.Failure<TradeDate>(new InvalidTradeDateError(date)));
        }

        public Result<List<TradeDate>> GetTradeDates()
        {
            var tradeDates = _tradeDateRepository.GetTradeDates();

            return (!tradeDates.IsNullOrEmpty()) ?
                Result.Success<List<TradeDate>>(tradeDates)
                : Result.Failure<List<TradeDate>>(new ApplicationError("Unable to get trade dates.  Validate trade dates are loaded into the system"));
        }
    }
}
