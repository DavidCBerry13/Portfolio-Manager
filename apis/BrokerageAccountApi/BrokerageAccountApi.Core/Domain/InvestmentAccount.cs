using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccountApi.Core.Domain
{
    public class InvestmentAccount
    {

        public String AccountNumber { get; set; }

        public String AccountName { get; set; }

        public AccountStatus AccountStatus { get; set; }

        public DateTime OpenDate { get; set; } 

        public DateTime? CloseDate { get; set; }

        public decimal CurrentValue { get; set; }



    }
}
