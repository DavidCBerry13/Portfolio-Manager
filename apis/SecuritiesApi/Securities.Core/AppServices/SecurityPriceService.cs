using DavidBerry.Framework;
using DavidBerry.Framework.ResultType;
using Securities.Core.AppInterfaces;
using Securities.Core.DataAccess;
using Securities.Core.Domain;
using Securities.Core.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.AppServices
{
    public class SecurityPriceService : ISecurityPriceService
    {

        public SecurityPriceService(ITradeDateRepository tradeDateRepository, ISecurityRepository securityRepository,
            ISecurityPriceRepository securityPriceRepository)
        {
            _tradeDateRepository = tradeDateRepository;
            _securityRepository = securityRepository;
            _securityPriceRepository = securityPriceRepository;
        }

        private readonly ITradeDateRepository _tradeDateRepository;
        private readonly ISecurityRepository _securityRepository;
        private readonly ISecurityPriceRepository _securityPriceRepository;


        public Result<List<SecurityPrice>> TryGetSecurityPrices(DateTime? date)
        {
            if ( date.HasValue)
            {
                // Use the provided date - it will be looked up to make sure it is a valid trade date
                return this.TryGetSecurityPrices(date.Value);
            }
            else
            {
                var tradeDate = _tradeDateRepository.GetLatestTradeDate();
                return this.TryGetSecurityPrices(tradeDate);
            }
        }



        internal Result<List<SecurityPrice>> TryGetSecurityPrices(DateTime date)
        {
            TradeDate tradeDate = _tradeDateRepository.GetTradeDate(date);
            if (tradeDate == null)
                Result.Failure<List<SecurityPrice>>(new InvalidTradeDateError(date));

            return TryGetSecurityPrices(tradeDate);
        }


        internal Result<List<SecurityPrice>> TryGetSecurityPrices(TradeDate tradeDate)
        {
            var securityPrices = _securityPriceRepository.GetSecurityPrices(tradeDate);
            return Result<List<SecurityPrice>>.Success(securityPrices);
        }




        public Result<SecurityPrice> TryGetSecurityPrice(string ticker, DateTime? date)
        {
            if ( date.HasValue)
            {
                // Use the provided date - it will be looked up to make sure it is a valid trade date
                return TryGetSecurityPrice(ticker, date.Value);
            }
            else
            {
                // Look up the latest trade date and use that
                var tradeDate = _tradeDateRepository.GetLatestTradeDate();
                return this.TryGetSecurityPrice(ticker, tradeDate);
            }

        }


        internal Result<SecurityPrice> TryGetSecurityPrice(string ticker, DateTime date)
        {
            TradeDate tradeDate = _tradeDateRepository.GetTradeDate(date);
            if (tradeDate == null)
            {
                return Result.Failure<SecurityPrice>(new InvalidTradeDateError(date));
            }

            return TryGetSecurityPrice(ticker, tradeDate);
        }


        internal Result<SecurityPrice> TryGetSecurityPrice(string ticker, TradeDate tradeDate)
        {

            Security security = _securityRepository.GetSecurity(ticker);
            if (security == null)
                return Result.Failure<SecurityPrice>(new TickerNotFoundError(ticker));

            if (tradeDate.Date.Date < security.FirstTradeDate.Date || tradeDate.Date.Date > security.LastTradeDate)
                return Result.Failure<SecurityPrice>(new InvalidDateForTickerError(ticker, tradeDate.Date, security.FirstTradeDate.Date, security.LastTradeDate));

            var securityPrice = _securityPriceRepository.GetSecurityPrice(ticker, tradeDate);
            return Result.Success<SecurityPrice>(securityPrice);
        }



    }
}
