using BrokerageAccountApi.Core.Domain;
using Framework.ResultType;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccountApi.Core.Services.Clients
{
    public interface IClientService
    {


        Result<Client> GetClient(int clientId);


        Result<List<Client>> GetClients();



        Result<Client> CreateClient(CreateClientCommand createClient);


    }
}
