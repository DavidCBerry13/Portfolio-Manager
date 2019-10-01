using BrokerageAccountApi.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccountApi.Core.DataAccess
{
    public interface IStateRepository
    {

        List<State> LoadStates();


        State LoadState(String stateCode);
        
    }
}
