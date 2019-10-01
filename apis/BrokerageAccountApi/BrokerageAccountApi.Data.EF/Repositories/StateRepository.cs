using BrokerageAccountApi.Core.DataAccess;
using BrokerageAccountApi.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrokerageAccountApi.Data.EF.Repositories
{
    public class StateRepository : IStateRepository
    {

        public StateRepository(BrokerageAccountDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        private BrokerageAccountDbContext _dbContext;


        public State LoadState(string stateCode)
        {
            return _dbContext.States
                .Where(s => s.StateCode == stateCode)
                .FirstOrDefault();
        }

        public List<State> LoadStates()
        {
            return _dbContext.States.ToList();
        }
    }
}
