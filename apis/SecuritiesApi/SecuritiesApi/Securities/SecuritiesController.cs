using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DavidBerry.Framework.ApiUtil.Controllers;
using DavidBerry.Framework.ApiUtil.Models;
using DavidBerry.Framework.Functional;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Securities.Core.AppInterfaces;
using Securities.Core.Domain;
using Securities.Core.Errors;
using SecuritiesApi.Common;

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
        /// <response code="200">Success.  A List of all securities is returned as a list of SecurityModel objects</response>
        /// <response code="500">Server error</response>
        [HttpGet(Name = "GetSecurities")]
        [ProducesResponseType(typeof(List<SecurityModel>), 200)]
        [ProducesResponseType(typeof(ServerErrorMessageModel), 500)]
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
                return HandleErrorResult(result);
            }
        }


        /// <summary>
        /// Checks if the API has data for the specified individual security and if it does, what dates the security has data for
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid Ticker.  The provided ticker is in an invalid format</response>
        /// <response code="400">Ticker Not Found.  The provided ticker did not correspond to any known security</response>
        [HttpGet("{ticker}", Name = "GetSecurity")]
        [ProducesResponseType(typeof(SecurityModel), 200)]
        [ProducesResponseType(typeof(ApiErrorMessageModel), 400)]
        [ProducesResponseType(typeof(TickerNotFoundMessageModel), 400)]
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
                return HandleErrorResult(result);
            }
        }



        protected override void LogErrorResult(Result result)
        {
            switch (result.Error)
            {
                case InvalidDataError error:
                    _logger.LogInformation(result.Error.Message);
                    break;
                case TickerNotFoundError error:
                    _logger.LogInformation(result.Error.Message);
                    break;
                case ApplicationError error:
                    _logger.LogError(result.Error.Message);
                    break;
                default:
                    _logger.LogError(result.Error.Message);
                    break;
            }
        }


        protected override ActionResult MapErrorResult(Result result)
        {
            switch (result.Error)
            {
                case InvalidDataError error:
                    return BadRequest(new ApiErrorMessageModel() { Message = result.Error.Message, ErrorCode = "INVALID_TICKER" });
                case TickerNotFoundError error:
                    return BadRequest(new TickerNotFoundMessageModel(error));
                case ApplicationError error:
                    return ControllerExtensions.InternalServerError(this, new ServerErrorMessageModel() );
                default:
                    return ControllerExtensions.InternalServerError(this, new ServerErrorMessageModel() );
            }
        }

    }
}