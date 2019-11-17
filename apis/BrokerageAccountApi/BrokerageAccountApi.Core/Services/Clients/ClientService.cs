using BrokerageAccountApi.Core.DataAccess;
using BrokerageAccountApi.Core.Domain;
using Framework.ResultType;
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

        public Result<Client> GetClient(int clientId)
        {
            var client = _clientRepository.LoadClient(clientId);
            return (client != null)
                ? Result.Success<Client>(client)
                : Result.Failure<Client>(new ObjectNotFoundError($"No client could be found with the id of {clientId}"));
        }

        public Result<List<Client>> GetClients()
        {
            return Result.Success<List<Client>>(_clientRepository.LoadClients(false));
        }

        public Result<Client> CreateClient(CreateClientCommand createClient)
        {
            State state = _stateRepository.LoadState(createClient.StateCode);
            if (state == null)
                return Result.Failure<Client>(new InvalidDataError($"No state found with the state code of {createClient.StateCode}"));


            Client client = new Client()
            {
                FirstName = createClient.FirstName,
                LastName = createClient.LastName,
                StreetAddress = createClient.StreetAddress,
                City = createClient.City,
                State = state,
                ZipCode = createClient.ZipCode,
                DateOfBirth = createClient.DateOfBirth,
                EmailAddress = createClient.EmailAddress,
                Phone = createClient.Phone
            };

            _clientRepository.InsertClient(client);

            return Result.Success<Client>(client);
        }
    }
}
