using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Framework.ApiUtil.Controllers;
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
            var securities = _securityService.GetSecurities();
            var models = _mapper.Map<List<Security>, List<SecurityModel>>(securities);

            return Ok(models);
        }


        /// <summary>
        /// Checks if the API has data for the specified individual security and if it does, what dates the security has data for
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        [HttpGet("{ticker}", Name = "GetSecurity")]
        public IActionResult Get(String ticker)
        {
            var security = _securityService.GetSecurity(ticker);
            var model = _mapper.Map<Security, SecurityModel>(security);

            return Ok(model);
        }

    }
}