using System;
using System.Collections.Generic;
using System.Text;
using BrokerageAccountApi.Core.DataAccess;
using BrokerageAccountApi.Core.Domain;

namespace BrokerageAccountApi.Core.Services
{
    public class StateService : IStateService
    {
        public StateService(IStateRepository repository)
        {
            _stateRepository = repository;
        }

        private IStateRepository _stateRepository;

        public State GetState(string stateCode)
        {
            return _stateRepository.LoadState(stateCode);
        }

        public List<State> GetStates()
        {
            return _stateRepository.LoadStates();
        }
    }
}
