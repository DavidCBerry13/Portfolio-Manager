using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccountApi.Core.Domain
{
    public class AccountStatus
    {

        public String AccountStatusCode { get; set; }

        public String AccountStatusName { get; set; }

        public bool IsOpen { get; set; }

    }
}
