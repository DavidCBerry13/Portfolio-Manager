using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using DavidBerry.Framework.Functional;
using DavidBerry.Framework.Util;
using Securities.Core.AppInterfaces;
using Securities.Core.DataAccess;
using Securities.Core.Domain;
using Securities.Core.Errors;

namespace Securities.Core.AppServices
{
    public class SecurityService : ISecurityService
    {


        public SecurityService(ISecurityRepository repository)
        {
            securityRepository = repository;
        }


        private readonly ISecurityRepository securityRepository;


        public Result<List<Security>> GetSecurities()
        {
            var securities = securityRepository.GetSecurities();
            return (!securities.IsNullOrEmpty()) ?
                Result.Success<List<Security>>(securities)
                : Result.Failure<List<Security>>(new ApplicationMissingDataError($"SecurityService.GetSecurities() did not find any securities when calling securityRepository.GetSecurities().  Security Repository is {securityRepository.GetType().FullName}.  Make sure security and price data is loaded in the database"));
        }


        /// <summary>
        /// Get the security that is represented by the given ticker symbol
        /// </summary>
        /// <param name="ticker">A string of the ticker</param>
        /// <returns>A Result/Success with the security object if the ticker is found.  A Result failure otherwise</returns>
        public Result<Security> GetSecurity(string ticker)
        {
            if (!Regex.IsMatch(ticker, "^[A-Z]{1,5}$", RegexOptions.IgnoreCase))
                return Result.Failure<Security>(new InvalidDataError("The string '{ticker}' is not a valid ticker string"));

            var security = securityRepository.GetSecurity(ticker);

            return security.Eval<Result<Security>>(
                value => Result.Success<Security>(value),
                () => Result.Failure<Security>(new TickerNotFoundError(ticker)));
        }

    }
}
