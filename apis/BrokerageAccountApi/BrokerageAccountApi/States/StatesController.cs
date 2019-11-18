using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokerageAccountApi.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrokerageAccountApi.States
{

    /// <summary>
    /// Enpoint to expose the master list of US States and Territories
    /// </summary>
    [Route("api/[controller]")]
    public class StatesController : ControllerBase
    {

        /// <summary>
        /// Creates a new StatesController class
        /// </summary>
        /// <param name="stateService">An IStateService object that encapsualtes the logic for getting state data</param>
        public StatesController(IStateService stateService)
        {
            _stateService = stateService;
        }


        private IStateService _stateService;


        /// <summary>
        /// Gets the list of US States and territories.  This is useful for populating dropdownsin forms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var states = _stateService.GetStates();

            var models = states.Select(s => new StateModel() { StateCode = s.StateCode, StateName = s.StateName }).ToList();
            return Ok(models);
        }


        /// <summary>
        /// Get the State object associated with the provided two character USPS state abbreviation
        /// </summary>
        /// <param name="stateCode">A two character USPS state abbreviation</param>
        /// <returns></returns>
        [HttpGet("{stateCode}", Name = "GetStates")]
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
