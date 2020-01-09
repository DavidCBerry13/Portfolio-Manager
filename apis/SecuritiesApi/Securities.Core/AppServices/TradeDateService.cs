using System;
using System.Collections.Generic;
using System.Text;
using DavidBerry.Framework.Functional;
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
            return (tradeDate != null) ?
                Result.Success<TradeDate>(tradeDate)
                : Result.Failure<TradeDate>("No trade dates are loaded into the system");
        }

        public Result<TradeDate> GetTradeDate(DateTime date)
        {
            var tradeDate = _tradeDateRepository.GetTradeDate(date.Date);
            return (tradeDate != null) ?
                Result.Success<TradeDate>(tradeDate)
                : Result.Failure<TradeDate>(new InvalidTradeDateError(date));

        }

        public Result<List<TradeDate>> GetTradeDates()
        {
            var tradeDates = _tradeDateRepository.GetTradeDates();
            return Result.Success<List<TradeDate>>(tradeDates);
        }
    }
}
