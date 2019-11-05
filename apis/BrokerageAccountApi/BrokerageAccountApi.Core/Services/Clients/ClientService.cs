using BrokerageAccountApi.Core.DataAccess;
using BrokerageAccountApi.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccountApi.Core.Services.Clients
{
    public class ClientService : IClientService
    {


        public ClientService(IClientRepository clientRepository, IStateRepository stateRepository)
        {
            _clientRepository = clientRepository;
            _stateRepository = stateRepository;
        }

        private IClientRepository _clientRepository;
        private IStateRepository _stateRepository;

        public Client GetClient(int clientId)
        {
            return _clientRepository.LoadClient(clientId);
        }

        public List<Client> GetClients()
        {
            return _clientRepository.LoadClients(false);
        }

        public Client CreateClient(CreateClientCommand createClient)
        {
            return null;
            //State state = _stateRepository.LoadState(createClient.StateCode);
            //if (state == null)
            //    throw new InvalidDataException();


            //Client client = new Client()
            //{
            //    FirstName = createClient.FirstName,
            //    LastName = createClient.LastName,
            //    StreetAddress = createClient.StreetAddress,

            //};

            //return client;
        }
    }
}
