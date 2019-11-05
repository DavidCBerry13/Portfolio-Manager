using BrokerageAccountApi.Core.DataAccess;
using BrokerageAccountApi.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Linq;

namespace BrokerageAccounts.Data.Dapper.Repositories
{
    public class StateRepository : BaseRepository, IStateRepository
    {

        public StateRepository(String connectionString) : base(connectionString)
        {
            
        }



        public static readonly String SELECT_ALL_STATES =
            @"SELECT StateCode, StateName 
                 FROM States";

        public static readonly String SELECT_STATE_BY_STATE_CODE =
            $@"{SELECT_ALL_STATES} 
                WHERE StateCode = @StateCode";

        public State LoadState(string stateCode)
        {
            using (var connection = this.GetConnection())
            {
                return connection.QuerySingle<State>(SELECT_STATE_BY_STATE_CODE, new { stateCode = stateCode });
            }
        }

        public List<State> LoadStates()
        {
            using (var connection = this.GetConnection())
            {
                return connection.Query<State>(SELECT_ALL_STATES).ToList();
            }
        }
    }
}
