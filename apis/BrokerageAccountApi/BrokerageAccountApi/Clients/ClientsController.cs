using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BrokerageAccountApi.Core.Domain;
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

        public ClientsController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        private readonly IMapper _mapper;
        private readonly IClientService _clientService;

        // GET: api/Clients
        [HttpGet(Name = "GetClients")]
        public ActionResult<ClientModel> Get()
        {
            var result = _clientService.GetClients();
            var models = _mapper.Map<List<Client>, List<ClientModel>>(result.Value);
            return Ok(models);
        }

        // GET: api/Clients/5
        [HttpGet("{clientId}", Name = "GetClientById")]
        public ActionResult<ClientModel> Get(int clientId)
        {
            var result = _clientService.GetClient(clientId);

            if ( result.IsSuccess)
            {                
                var model = _mapper.Map<Client, ClientModel>(result.Value);
                return Ok(model);
            }
            return NotFound(result.Error.Message);
        }

        // POST: api/Clients
        [HttpPost]
        public ActionResult<ClientModel> Post([FromBody]CreateClientModel createClientModel)
        {
            var command = new CreateClientCommand()
            {
                FirstName = createClientModel.FirstName,
                LastName = createClientModel.LastName,
                StreetAddress = createClientModel.StreetAddress,
                City = createClientModel.City,
                StateCode = createClientModel.StateCode,
                ZipCode = createClientModel.ZipCode,
                DateOfBirth = createClientModel.DateOfBirth,
                EmailAddress = createClientModel.EmailAddress,
                Phone = createClientModel.Phone
            };

            var result = _clientService.CreateClient(command);

            if (result.IsSuccess)
            {
                var client = result.Value;
                var model = _mapper.Map<Client, ClientModel>(result.Value);
                return CreatedAtRoute("GetClientById", new { clientId = client.ClientId }, client);
            }
            else
            {
                return BadRequest(result.Error.Message);
            }
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
