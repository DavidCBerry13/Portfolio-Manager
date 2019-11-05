using BrokerageAccountApi.Core.DataAccess;
using BrokerageAccountApi.Core.Domain;
using Dapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccounts.Data.Dapper.Repositories
{
    public class ClientRepository : BaseRepository, IClientRepository
    {

        public ClientRepository(String connectionString) : base(connectionString)
        {

        }


        public static readonly String SELECT_ALL_CLIENTS =
            @"WITH CurrentTradeDate AS
              (
                  SELECT
              	    MAX(TradeDate) As CurrentTradeDate
              		FROM TradeDates
              )
              SELECT
                    c.ClientId,
              		c.FirstName,
              		c.LastName,
              		c.StreetAddress,
              		c.City,
              		c.ZipCode,
              		c.Email,
              		c.Phone,
                    s.StateCode,
                    s.StateName,
              		a.AccountNumber,
              		a.AccountName,
              		a.OpenDate,
              		a.CloseDate,              		
              		mv.MarketValue AS CurrentValue,
                    a.AccountStatusCode,
                    sc.AccountStatusName,
                    sc.IsOpen
                  FROM Clients c
                  INNER JOIN States s
                      ON c.StateCode = s.StateCode
              	  INNER JOIN InvestmentAccounts a
              	      ON c.ClientId = a.ClientId
                  INNER JOIN AccountStatusCodes sc
                      ON sc.AccountStatusCode = a.AccountStatusCode
              	  INNER JOIN CurrentTradeDate d	   
              	      ON 1 = 1 
              	  LEFT OUTER JOIN AccountMarketValues mv
              	      ON mv.AccountNumber = a.AccountNumber
              		  AND mv.TradeDate = d.CurrentTradeDate
             ";


        public static readonly String SELECT_CLIENT_BY_CLIENT_ID =
            $@"{SELECT_ALL_CLIENTS}
                   WHERE 
                       c.ClientId = @ClientId
               ";


        public static readonly String INSERT_CLIENT =
            $@"INSERT INTO Clients (FirstName, LastName, StreetAddress, City, StateCode, ZipCode, Email, Phone, DateOfBirth)
                   VALUES (@FirstName, @LastName, @StreetAddress, @City, @StateCode, @ZipCode, @Email, @Phone, @DateOfBirth)
              ";


        public Client LoadClient(int clientId)
        {
            var clients = new Dictionary<int, Client>();

            using (var connection = GetConnection())
            {
                return connection.Query<Client, State, InvestmentAccount, AccountStatus, Client>(
                    SELECT_CLIENT_BY_CLIENT_ID,
                    (client, state, account, accountStatus) =>
                    {
                        if (!clients.ContainsKey(client.ClientId))
                        {
                            // First time we have seen this client
                            client.State = state;
                            account.AccountStatus = accountStatus;
                            client.Accounts.Add(account);

                            clients.Add(client.ClientId, client);
                            return client;
                        }
                        else
                        {
                            var existingClient = clients[client.ClientId];
                            account.AccountStatus = accountStatus;
                            existingClient.Accounts.Add(account);
                            return existingClient;
                        }
                    },
                    new { ClientId = clientId },
                    splitOn: "StateCode, AccountNumber, AccountStatusCode"
                    )
                    .Distinct()
                    .SingleOrDefault();
            }
        }

        public List<Client> LoadClients(bool includeInactive)
        {

            var clients = new Dictionary<int, Client>();

            using (var connection = GetConnection())
            {
                return connection.Query<Client, State, InvestmentAccount, AccountStatus, Client>(
                    SELECT_ALL_CLIENTS,
                    (client, state, account, accountStatus) =>
                    {                       
                        if (!clients.ContainsKey(client.ClientId))
                        {
                            // First time we have seen this client
                            client.State = state;
                            account.AccountStatus = accountStatus;
                            client.Accounts.Add(account);

                            clients.Add(client.ClientId, client);                            
                            return client;
                        }
                        else
                        {
                            var existingClient = clients[client.ClientId];
                            account.AccountStatus = accountStatus;
                            existingClient.Accounts.Add(account);
                            return existingClient;
                        }
                    },
                    splitOn: "StateCode, AccountNumber, AccountStatusCode"
                    )
                    .Distinct()
                    .ToList();
            }
        }



        public void InsertClient(Client client)
        {
            using (var connection = GetConnection())
            {
                connection.Execute(INSERT_CLIENT,
                    new
                    {
                        FirstName = client.FirstName,
                        LastName = client.LastName,
                        StreetAddress = client.StreetAddress,
                        City = client.City,
                        StateCode = client.State.StateCode,
                        ZipCode = client.ZipCode,
                        Email = client.EmailAddress,
                        Phone = client.Phone,
                        DateOfBirth = client.DateOfBirth
                    });

                int newClientid = connection.ExecuteScalar<int>("SELECT SCOPE_IDENTITY()");

                client.ClientId = newClientid;
            }
        }
    }
}
