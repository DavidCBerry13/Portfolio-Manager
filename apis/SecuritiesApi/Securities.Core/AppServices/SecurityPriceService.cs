using Framework;
using Framework.OperationResults;
using Securities.Core.AppInterfaces;
using Securities.Core.DataAccess;
using Securities.Core.Domain;
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


        public Result<SecurityPriceErrorCode> TryGetSecurityPrices(DateTime? date, out List<SecurityPrice> securityPrices)
        {
            if ( date.HasValue)
            {
                // Use the provided date - it will be looked up to make sure it is a valid trade date
                return this.TryGetSecurityPrices(date.Value, out securityPrices);
            }
            else
            {
                var tradeDate = _tradeDateRepository.GetLatestTradeDate();
                return this.TryGetSecurityPrices(tradeDate, out securityPrices);
            }
        }



        internal Result<SecurityPriceErrorCode> TryGetSecurityPrices(DateTime date, out List<SecurityPrice> securityPrices)
        {
            TradeDate tradeDate = _tradeDateRepository.GetTradeDate(date);
            if (tradeDate == null)
                Result<SecurityPriceErrorCode>.Error(SecurityPriceErrorCode.TRADE_DATE_NOT_VALID, $"Trade date of {date:yyyy-MM-dd} does not exist");

            return this.TryGetSecurityPrices(tradeDate, out securityPrices);
        }


        internal Result<SecurityPriceErrorCode> TryGetSecurityPrices(TradeDate tradeDate, out List<SecurityPrice> securityPrices)
        {
            securityPrices = _securityPriceRepository.GetSecurityPrices(tradeDate);
            return Result<SecurityPriceErrorCode>.Success();            
        }




        public Result<SecurityPriceErrorCode> TryGetSecurityPrice(string ticker, DateTime? date, out SecurityPrice securityPrice)
        {
            if ( date.HasValue)
            {
                // Use the provided date - it will be looked up to make sure it is a valid trade date
                return this.TryGetSecurityPrice(ticker, date.Value, out securityPrice);
            }
            else
            {
                // Look up the latest trade date and use that
                var tradeDate = _tradeDateRepository.GetLatestTradeDate();
                return this.TryGetSecurityPrice(ticker, tradeDate, out securityPrice);
            }
                
        }


        internal Result<SecurityPriceErrorCode> TryGetSecurityPrice(string ticker, DateTime date, out SecurityPrice securityPrice)
        {
            TradeDate tradeDate = _tradeDateRepository.GetTradeDate(date);
            if (tradeDate == null)
            {
                securityPrice = null;
                return Result<SecurityPriceErrorCode>.Error(SecurityPriceErrorCode.TRADE_DATE_NOT_VALID, $"The trade date of {date:yyyy-MM-dd} does not exist");
            }

            return TryGetSecurityPrice(ticker, tradeDate, out securityPrice);
        }


        internal Result<SecurityPriceErrorCode> TryGetSecurityPrice(string ticker, TradeDate tradeDate, out SecurityPrice securityPrice)
        {
            securityPrice = null;

            Security security = _securityRepository.GetSecurity(ticker);
            if (security == null)
                return Result<SecurityPriceErrorCode>.Error(SecurityPriceErrorCode.TICKER_DOES_NOT_EXIST, $"There is no security associated with the ticker {ticker}");

            if (tradeDate.Date.Date < security.FirstTradeDate.Date || tradeDate.Date.Date > security.LastTradeDate)
                return Result<SecurityPriceErrorCode>.Error(SecurityPriceErrorCode.NO_DATA_FOR_TICKER_ON_TRADE_DATE, $"There is no data for the security {ticker} on the date of {tradeDate.Date:yyyy-MM-dd}.  The security has data from {security.FirstTradeDate:yyyy-MM-dd} to {security.LastTradeDate:yyyy-MM-dd}");

            securityPrice = _securityPriceRepository.GetSecurityPrice(ticker, tradeDate);
            return Result<SecurityPriceErrorCode>.Success();
        }



    }
}
