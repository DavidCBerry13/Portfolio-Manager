using BrokerageAccountApi.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccountApi.Core.Services
{
    public interface IStateService
    {

        List<State> GetStates();


        State GetState(String stateCode);

    }
}
