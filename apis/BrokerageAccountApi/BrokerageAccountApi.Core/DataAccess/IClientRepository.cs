using BrokerageAccountApi.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccountApi.Core.DataAccess
{
    public interface IClientRepository
    {


        List<Client> LoadClients(bool activeOnly = false);


        Client LoadClient(int clientId);


        void InsertClient(Client client);
    }
}
