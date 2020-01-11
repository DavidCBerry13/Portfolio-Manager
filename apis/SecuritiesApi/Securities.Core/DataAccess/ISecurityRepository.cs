using DavidBerry.Framework.Functional;
using Securities.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.DataAccess
{
    public interface ISecurityRepository
    {


        List<Security> GetSecurities();

        List<Security> GetSecurities(IEnumerable<string> tickers);

        Maybe<Security> GetSecurity(String ticker);
    }
}
