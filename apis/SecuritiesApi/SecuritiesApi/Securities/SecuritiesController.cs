using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DavidBerry.Framework.ApiUtil.Controllers;
using DavidBerry.Framework.ApiUtil.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Securities.Core.AppInterfaces;
using Securities.Core.Domain;

namespace SecuritiesApi.Securities
{


    /// <summary>
    /// Provides data about what securities have data available through this API
    /// </summary>
    [Route("api/[controller]")]
    public class SecuritiesController : ApiControllerBase
    {


        public SecuritiesController(ILogger<SecuritiesController> logger, IMapper mapper, ISecurityService securityService)
            : base(logger, mapper)
        {
            _securityService = securityService;
        }


        private readonly ISecurityService _securityService;


        /// <summary>
        /// Gets a list of all securities that have data in the system, including what dates each security has data for
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetSecurities")]
        public IActionResult Get()
        {
            var result = _securityService.GetSecurities();
            if (result.IsSuccess)
            {
                var models = _mapper.Map<List<Security>, List<SecurityModel>>(result.Value);
                return Ok(models);
            }
            else
            {
                return ControllerExtensions.InternalServerError(this, new ApiMessageModel() { Message = result.Error.Message });
            }


        }


        /// <summary>
        /// Checks if the API has data for the specified individual security and if it does, what dates the security has data for
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        [HttpGet("{ticker}", Name = "GetSecurity")]
        public IActionResult Get(String ticker)
        {
            var result = _securityService.GetSecurity(ticker);
            if (result.IsSuccess)
            {
                var model = _mapper.Map<Security, SecurityModel>(result.Value);
                return Ok(model);
            }
            else
            {
                return BadRequest(result.Error.Message);
            }
        }

    }
}