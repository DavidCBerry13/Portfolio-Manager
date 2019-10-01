using System;
using System.Collections.Generic;
using System.Text;
using Securities.Core.AppInterfaces;
using Securities.Core.DataAccess;
using Securities.Core.Domain;

namespace Securities.Core.AppServices
{
    public class SecurityService : ISecurityService
    {


        public SecurityService(ISecurityRepository repository)
        {
            securityRepository = repository;
        }


        private readonly ISecurityRepository securityRepository;


        public List<Security> GetSecurities()
        {
            return securityRepository.GetSecurities();
        }

        public Security GetSecurity(string ticker)
        {
            return securityRepository.GetSecurity(ticker);
        }
    }
}
