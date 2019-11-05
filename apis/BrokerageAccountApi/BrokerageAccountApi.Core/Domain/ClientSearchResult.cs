using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccountApi.Core.Domain
{
    public class ClientSearchResult
    {

        public int ClientId { get; set; }


        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String StreetAddress { get; set; }

        public String City { get; set; }

        public State State { get; set; }

        public String ZipCode { get; set; }

        public int ActiveAccounts { get; set; }

        public decimal CurrentAccountsValue { get; set; }

    }
}
