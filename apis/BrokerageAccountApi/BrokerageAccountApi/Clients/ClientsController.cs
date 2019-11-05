using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokerageAccountApi.Core.Services;
using BrokerageAccountApi.Core.Services.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrokerageAccountApi.Clients
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }


        private readonly IClientService _clientService;

        // GET: api/Clients
        [HttpGet(Name = "GetClients")]
        public ActionResult<ClientModel> Get()
        {
            var clients = _clientService.GetClients();

            var models = clients.Select(c => new ClientModel()
            {
                ClientId = c.ClientId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                StreetAddress = c.StreetAddress,
                City = c.City,
                StateCode = c.State.StateCode,
                ZipCode = c.ZipCode,
                Accounts = c.Accounts.Select(a => new ClientModel.ClientAccountModel()
                    {
                        AccountNumber = a.AccountNumber,
                        AccountName = a.AccountName,
                        AccountStatus = a.AccountStatus.AccountStatusName,
                        OpenDate = a.OpenDate.ToString("yyyy-MM-dd"),
                        CloseDate = a.CloseDate.HasValue ? a.CloseDate.Value.ToString("yyyy-MM-dd") : String.Empty,
                        AccountBalance = a.CurrentValue
                    }
                ).ToList()
            });

            return Ok(models);
        }

        // GET: api/Clients/5
        [HttpGet("{clientId}", Name = "GetClientById")]
        public ActionResult<ClientModel> Get(int clientId)
        {
            var client = _clientService.GetClient(clientId);

            var model = new ClientModel()
            {
                ClientId = client.ClientId,
                FirstName = client.FirstName,
                LastName = client.LastName,
                StreetAddress = client.StreetAddress,
                City = client.City,
                StateCode = client.State.StateCode,
                ZipCode = client.ZipCode,
                Accounts = client.Accounts.Select(a => new ClientModel.ClientAccountModel()
                {
                    AccountNumber = a.AccountNumber,
                    AccountName = a.AccountName,
                    AccountStatus = a.AccountStatus.AccountStatusName,
                    OpenDate = a.OpenDate.ToString("yyyy-MM-dd"),
                    CloseDate = a.CloseDate.HasValue ? a.CloseDate.Value.ToString("yyyy-MM-dd") : String.Empty,
                    AccountBalance = a.CurrentValue
                }).ToList()
            };

            return Ok(model);
        }

        // POST: api/Clients
        [HttpPost]
        public void Post([FromBody]CreateClientModel createClientModel)
        {



        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
