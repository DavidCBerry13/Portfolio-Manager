using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokerageAccountApi.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrokerageAccountApi.States
{
    [Route("api/[controller]")]
   
    public class StatesController : ControllerBase
    {

        public StatesController(IStateService stateService)
        {
            _stateService = stateService;
        }


        private IStateService _stateService;


        // GET: api/States
        [HttpGet]
        public IActionResult Get()
        {
            var states = _stateService.GetStates();

            var models = states.Select(s => new StateModel() { StateCode = s.StateCode, StateName = s.StateName }).ToList();
            return Ok(models);
        }

        // GET: api/States/5
        [HttpGet("{stateCode}", Name = "Get")]
        public IActionResult Get(String stateCode)
        {
            var state = _stateService.GetState(stateCode);
            if (state == null)
                return NotFound($"No state found with the state code of {stateCode}");

            var model = new StateModel() { StateCode = state.StateCode, StateName = state.StateName };
            return Ok(model);
        }


    }
}
