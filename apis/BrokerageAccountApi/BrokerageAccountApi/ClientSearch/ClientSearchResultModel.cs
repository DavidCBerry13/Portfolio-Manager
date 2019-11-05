using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerageAccountApi.ClientSearch
{
    public class ClientSearchResultModel
    {

        public int ClientId { get; set; }


        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String StreetAddress { get; set; }

        public String City { get; set; }

        public String StateCode { get; set; }

        public String ZipCode { get; set; }

        public int ActiveAccounts { get; set; }

        public decimal CurrentAccountsValue { get; set; }

    }
}
