using DavidBerry.Framework;
using DavidBerry.Framework.Functional;
using Securities.Core.AppInterfaces;
using Securities.Core.DataAccess;
using Securities.Core.Domain;
using Securities.Core.Errors;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using DavidBerry.Framework.Util;
using System.Text.RegularExpressions;

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


        public Result<List<SecurityPrice>> GetSecurityPrices(DateTime? date)
        {
            if ( date.HasValue)
            {
                // Use the provided date - it will be looked up to make sure it is a valid trade date
                return this.GetSecurityPrices(date.Value);
            }
            else
            {
                var tradeDate = _tradeDateRepository.GetLatestTradeDate();
                return tradeDate.Eval<Result<List<SecurityPrice>>>(
                    d => this.GetSecurityPrices(d),
                    () => Result.Failure<List<SecurityPrice>>(new MissingDataError("No trade dates are loaded into the system")));
            }
        }



        internal Result<List<SecurityPrice>> GetSecurityPrices(DateTime date)
        {
            Maybe<TradeDate> tradeDate = _tradeDateRepository.GetTradeDate(date);

            if (tradeDate.HasValue)
                return GetSecurityPrices(tradeDate.Value);
            else
                return Result.Failure<List<SecurityPrice>>(new InvalidTradeDateError(date));
        }


        internal Result<List<SecurityPrice>> GetSecurityPrices(TradeDate tradeDate)
        {
            var securityPrices = _securityPriceRepository.GetSecurityPrices(tradeDate);

            return (!securityPrices.IsNullOrEmpty()) ?
                Result<List<SecurityPrice>>.Success(securityPrices)
                : Result.Failure<List<SecurityPrice>>(new MissingDataError($"No security price data exists for the trade date {tradeDate.AsString()}.  Check to see if the data has been loaded or is missing"));
        }




        public Result<SecurityPrice> GetSecurityPrice(string ticker, DateTime? date)
        {
            if ( date.HasValue)
            {
                // Use the provided date - it will be looked up to make sure it is a valid trade date
                return GetSecurityPrice(ticker, date.Value);
            }
            else
            {
                // Look up the latest trade date and use that
                var tradeDate = _tradeDateRepository.GetLatestTradeDate();
                return tradeDate.Eval<Result<SecurityPrice>>(
                    d => GetSecurityPrice(ticker, d),
                    () => Result.Failure<SecurityPrice>(new MissingDataError("No trade dates are in the system")));
            }

        }


        internal Result<SecurityPrice> GetSecurityPrice(string ticker, DateTime date)
        {
            Maybe<TradeDate> tradeDate = _tradeDateRepository.GetTradeDate(date);

            return tradeDate.Eval<Result<SecurityPrice>>(
                d => GetSecurityPrice(ticker, d),
                () => Result.Failure<SecurityPrice>(new InvalidTradeDateError(date)));
        }


        internal Result<SecurityPrice> GetSecurityPrice(string ticker, TradeDate tradeDate)
        {

            Maybe<Security> security = _securityRepository.GetSecurity(ticker);
            if (!security.HasValue)
                return Result.Failure<SecurityPrice>(new TickerNotFoundError(ticker));

            if (tradeDate.Date.Date < security.Value.FirstTradeDate.Date || tradeDate.Date.Date > security.Value.LastTradeDate)
                return Result.Failure<SecurityPrice>(new InvalidDateForTickerError(tradeDate, security.Value));

            var securityPrice = _securityPriceRepository.GetSecurityPrice(ticker, tradeDate);
            return securityPrice.Eval<Result<SecurityPrice>>(
                value => Result.Success<SecurityPrice>(value),
                () => Result.Failure<SecurityPrice>(new MissingDataError($"No data found for ticker {ticker} on trade date {tradeDate.Date}")));
        }




        public Result<List<SecurityPrice>> GetSecurityPrices(DateTime? date, IEnumerable<string> tickers)
        {
            if (date.HasValue)
            {
                // Use the provided date - it will be looked up to make sure it is a valid trade date
                return GetSecurityPrices(date.Value, tickers);
            }
            else
            {
                // Look up the latest trade date and use that
                var tradeDate = _tradeDateRepository.GetLatestTradeDate();
                return tradeDate.Eval<Result<List<SecurityPrice>>>(
                    value => GetSecurityPrices(value, tickers),
                    () => Result.Failure<List<SecurityPrice>>(new MissingDataError("No trade dates are in the system")));
            }
        }


        internal Result<List<SecurityPrice>> GetSecurityPrices(DateTime date, IEnumerable<string> tickers)
        {
            Maybe<TradeDate> tradeDate = _tradeDateRepository.GetTradeDate(date);

            if (tradeDate.HasValue)
                return GetSecurityPrices(tradeDate.Value, tickers);
            else
                return Result.Failure<List<SecurityPrice>>(new InvalidTradeDateError(date));
        }


        internal Result<List<SecurityPrice>> GetSecurityPrices(TradeDate tradeDate, IEnumerable<string> tickers)
        {
            // Validate the tickers are real.  Either all the tickers are real and have data for this date or we
            // return a failure (there is no partial success).
            var securities = _securityRepository.GetSecurities(tickers);

            var unknownTickers = tickers.Except(securities.Select(s => s.Ticker));
            if (unknownTickers.Any())
                return Result.Failure<List<SecurityPrice>>(new TickerNotFoundError(unknownTickers));

            var invalidDateSecurities = securities.Where(s => tradeDate.Date < s.FirstTradeDate.Date || tradeDate.Date > s.LastTradeDate);
            if (invalidDateSecurities.Any())
                return Result.Failure<List<SecurityPrice>>(new InvalidDateForTickerError(tradeDate, invalidDateSecurities));

            var securityPrices = _securityPriceRepository.GetSecurityPrices(tradeDate, tickers);
            return Result<List<SecurityPrice>>.Success(securityPrices);
        }

        public Result<List<SecurityPrice>> GetSecurityPrices(string ticker, DateTime? startDate, DateTime? endDate)
        {
            // Make sure the ticker is in a valid format
            if (!Regex.IsMatch(ticker, "^[A-Z]{1,5}$", RegexOptions.IgnoreCase))
                return Result.Failure<List<SecurityPrice>>(new InvalidTickerFormatError(ticker));

            // Make sure the ticker exists (corresponds to a security)
            var security = _securityRepository.GetSecurity(ticker);
            if (!security.HasValue)
                return Result.Failure<List<SecurityPrice>>(new TickerNotFoundError(ticker));

            var latestTradeDate = _tradeDateRepository.GetLatestTradeDate();

            endDate = endDate ?? latestTradeDate.Value.Date;  // If no end date, then go through latest trade date
            startDate = startDate ?? endDate.Value.AddDays(-90);  // If no start date, then go 90 days back from end date

            var securityPrices = _securityPriceRepository.GetSecurityPrices(ticker, startDate.Value, endDate.Value);
            return Result<List<SecurityPrice>>.Success(securityPrices.OrderBy(s => s.TradeDate).ToList());
        }
    }
}
