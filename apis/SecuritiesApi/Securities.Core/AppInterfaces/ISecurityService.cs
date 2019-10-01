using Securities.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.AppInterfaces
{
    public interface ISecurityService
    {


        List<Security> GetSecurities();


        Security GetSecurity(String ticker);
    }
}
