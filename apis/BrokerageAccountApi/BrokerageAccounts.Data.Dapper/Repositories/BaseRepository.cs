using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BrokerageAccounts.Data.Dapper.Repositories
{
    public abstract class BaseRepository
    {

        public BaseRepository(String connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly String _connectionString;



        public SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

    }
}
