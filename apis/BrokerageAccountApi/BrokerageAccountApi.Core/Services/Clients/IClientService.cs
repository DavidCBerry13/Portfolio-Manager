using BrokerageAccountApi.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccountApi.Core.Services.Clients
{
    public interface IClientService
    {


        Client GetClient(int clientId);


        List<Client> GetClients();



        Client CreateClient(CreateClientCommand createClient);


    }
}
