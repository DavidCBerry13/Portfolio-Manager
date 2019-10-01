using System;
using System.Collections.Generic;
using System.Text;
using Securities.Core.AppInterfaces;
using Securities.Core.DataAccess;
using Securities.Core.Domain;

namespace Securities.Core.AppServices
{
    public class TradeDateService : ITradeDateService
    {

        public TradeDateService(ITradeDateRepository repository)
        {
            _tradeDateRepository = repository;
        }

        private readonly ITradeDateRepository _tradeDateRepository;

        public TradeDate GetLatestTradeDate()
        {
            return _tradeDateRepository.GetLatestTradeDate();
        }

        public TradeDate GetTradeDate(DateTime date)
        {
            return _tradeDateRepository.GetTradeDate(date);
        }

        public List<TradeDate> GetTradeDates()
        {
            return _tradeDateRepository.GetTradeDates();
        }
    }
}
