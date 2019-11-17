using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccountApi.Core.Domain
{
    /// <summary>
    /// Represents a client (person) with one or more investment accounts at the firm
    /// </summary>
    public class Client
    {

        public Client()
        {
            Accounts = new List<InvestmentAccount>();
        }


        public const String FIRST_NAME_VALIDATION = @"^[A-Z][A-Z ]*[A-Z]$";

        public const String LAST_NAME_VALIDATION = @"^[A-Z][A-Z \.-]*[A-Z]$";



        public int ClientId { get; set; }


        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String StreetAddress { get; set; }

        public String City { get; set; }

        public State State { get; set; }

        public String ZipCode { get; set; }

        public String EmailAddress { get; set; }

        public String Phone { get; set; }

        public DateTime DateOfBirth { get; set; }

        public List<InvestmentAccount> Accounts { get; set; }

    }
}
