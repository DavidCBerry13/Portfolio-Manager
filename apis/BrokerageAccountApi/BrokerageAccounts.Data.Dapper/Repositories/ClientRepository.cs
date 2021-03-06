﻿using BrokerageAccountApi.Core.DataAccess;
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
              	  LEFT OUTER JOIN InvestmentAccounts a
              	      ON c.ClientId = a.ClientId
                  LEFT OUTER JOIN AccountStatusCodes sc
                      ON sc.AccountStatusCode = a.AccountStatusCode
              	  INNER JOIN CurrentTradeDate d	   
              	      ON 1 = 1 
              	  LEFT OUTER JOIN AccountMarketValues mv
              	      ON mv.AccountNumber = a.AccountNumber
              		  AND mv.TradeDate = d.CurrentTradeDate
             ";


        public static readonly String SELECT_ONLY_ACTIVE_CLIENTS =
            $@"{SELECT_ALL_CLIENTS}
                   WHERE
				       a.AccountNumber IS NOT NULL
				      AND sc.IsOpen = 1
              ";


        public static readonly String SELECT_CLIENT_BY_CLIENT_ID =
            $@"{SELECT_ALL_CLIENTS}
                   WHERE 
                       c.ClientId = @ClientId
               ";


        public static readonly String INSERT_CLIENT =
            $@"INSERT INTO Clients (FirstName, LastName, StreetAddress, City, StateCode, ZipCode, Email, Phone, DateOfBirth)
                   VALUES (@FirstName, @LastName, @StreetAddress, @City, @StateCode, @ZipCode, @Email, @Phone, @DateOfBirth);
               SELECT CAST(SCOPE_IDENTITY() as int)
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

        public List<Client> LoadClients(bool activeOnly = false)
        {
            var clients = new Dictionary<int, Client>();
            String sql = activeOnly ? SELECT_ONLY_ACTIVE_CLIENTS : SELECT_ALL_CLIENTS;

            using (var connection = GetConnection())
            {
                return connection.Query<Client, State, InvestmentAccount, AccountStatus, Client>(
                    sql,
                    (client, state, account, accountStatus) =>
                    {                       
                        if (!clients.ContainsKey(client.ClientId))
                        {
                            // First time we have seen this client
                            client.State = state;
                            if (account != null)  // This is for the case when a client has no accounts at all
                            {
                                account.AccountStatus = accountStatus;
                                client.Accounts.Add(account);
                            }
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
                var newClientId = connection.Query<int>(INSERT_CLIENT,
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
                    }).Single();
                client.ClientId = newClientId;
            }
        }
    }
}
