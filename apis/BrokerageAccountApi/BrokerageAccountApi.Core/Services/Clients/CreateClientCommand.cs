using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccountApi.Core.Services.Clients
{
    public class CreateClientCommand
    {
        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String StreetAddress { get; set; }

        public String City { get; set; }

        public String StateCode { get; set; }

        public String ZipCode { get; set; }

        public String EmailAddress { get; set; }

        public String Phone { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
