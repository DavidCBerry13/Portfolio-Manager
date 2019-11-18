using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BrokerageAccountApi.Core.Domain;
using BrokerageAccountApi.Core.Services;
using BrokerageAccountApi.Core.Services.Clients;
using Framework.ApiUtil.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BrokerageAccountApi.Clients
{

    /// <summary>
    /// Provides information about clients with investment accounts
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ApiControllerBase
    {

        public ClientsController(IClientService clientService, ILogger<ApiControllerBase> logger, IMapper mapper)
            : base(logger, mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        private readonly IClientService _clientService;

        /// <summary>
        /// Gets a list of all clients with investment accounts
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetClients")]
        public ActionResult<ClientModel> Get()
        {
            var result = _clientService.GetClients();
            var models = _mapper.Map<List<Client>, List<ClientModel>>(result.Value);
            return Ok(models);
        }

        /// <summary>
        /// Gets the client record for the client with the given id
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates a new client, initially without any accounts
        /// </summary>
        /// <param name="createClientModel"></param>
        /// <returns></returns>
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
                var model = _mapper.Map<Client, ClientModel>(result.Value);
                return CreatedAtRoute("GetClientById", new { clientId = model.ClientId }, model);
            }
            else
            {
                return MapErrorResult<Client, ClientModel>(result);
            }
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


    }
}
