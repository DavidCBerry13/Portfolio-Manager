using BrokerageAccountApi.Core.Domain;
using DavidBerry.Framework.ResultType;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccountApi.Core.Services.Clients
{
    public interface IClientService
    {


        Result<Client> GetClient(int clientId);


        Result<List<Client>> GetClients(bool activeOnly = false);



        Result<Client> CreateClient(CreateClientCommand createClient);


    }
}
