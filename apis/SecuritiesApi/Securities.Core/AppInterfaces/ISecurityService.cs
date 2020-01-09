using DavidBerry.Framework.ResultType;
using Securities.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.AppInterfaces
{
    public interface ISecurityService
    {


        Result<List<Security>> GetSecurities();


        Result<Security> GetSecurity(String ticker);
    }
}
