﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccountApi.Core.Domain
{
    public class Client
    {

        public Client()
        {
            Accounts = new List<InvestmentAccount>();
        }


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
