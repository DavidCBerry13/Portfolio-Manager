using System;
using System.Collections.Generic;
using System.Text;
using DavidBerry.Framework.ResultType;
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
            return Result.Success<List<Security>>(securityRepository.GetSecurities());
        }

        public Result<Security> GetSecurity(string ticker)
        {
            var security = securityRepository.GetSecurity(ticker);
            return (security != null) ?
                Result.Success<Security>(security)
                : Result.Failure<Security>(new TickerNotFoundError(ticker));

        }
    }
}
