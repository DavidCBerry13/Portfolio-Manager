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

namespace SecuritiesApi.TradeDates
{
    [Route("api/[controller]")]
    public class TradeDatesController : ApiControllerBase
    {


        public TradeDatesController(ILogger<TradeDatesController> logger, IMapper mapper, ITradeDateService tradeDateService)
            : base(logger, mapper)
        {
            _tradeDateService = tradeDateService;
        }


        private readonly ITradeDateService _tradeDateService;



        /// <summary>
        /// Gets a list of all trade dates in the system, that is, dates where US Securities were traded and we have data for
        /// </summary>
        /// <returns>A List of TradeDateModel objects</returns>
        /// <response code="200">Success.  A list of TradeDateModel objects representing all available trade dates will be returned</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet(Name = "GetTradeDates")]
        [ProducesResponseType(typeof(TradeDateModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Get()
        {
            var result = _tradeDateService.GetTradeDates();

            if (result.IsSuccess)
            {
                var models = _mapper.Map<List<TradeDate>, List<TradeDateModel>>(result.Value);
                return Ok(models);
            }
            else
            {
                return HandleErrorResult(result);
            }
        }


        /// <summary>
        /// Gets the trade date information associated with the given date.  This method can also be used to check if a date is a valid trade date
        /// </summary>
        /// <param name="date">A Date value</param>
        /// <returns></returns>
        /// <response code="200">Success.  The corresponding TradeDateModel object will be returned</response>
        /// <response code="400">Bad Request.  No corresponding trade date can be found for the provided date.  Most likely this means the provided date is not a valid trade date (for example, a weekend or holiday)</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{date:datetime}", Name = "GetTradeDate")]
        [ProducesResponseType(typeof(TradeDateModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 400)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult Get(DateTime date)
        {
            var result = _tradeDateService.GetTradeDate(date.Date);
            if ( result.IsSuccess)
            {
                var model = _mapper.Map<TradeDate, TradeDateModel>(result.Value);
                return Ok(model);
            }
            else
            {
                return HandleErrorResult(result);
            }
        }


        /// <summary>
        /// Gets the latest (most current) trade date loaded in the system
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success.  A TradeDateModel representing the most recent trade date is returned</response>
        /// <response code="500">Internal application error.  An error occurred on the server</response>
        [HttpGet("Latest", Name = "GetLatestTradeDate")]
        [ProducesResponseType(typeof(TradeDateModel), 200)]
        [ProducesResponseType(typeof(ApiMessageModel), 500)]
        public IActionResult GetLatestTradeDate()
        {
            var result = _tradeDateService.GetLatestTradeDate();

            if (result.IsSuccess)
            {
                var model = _mapper.Map<TradeDate, TradeDateModel>(result.Value);
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
                case InvalidTradeDateError error:
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
                case InvalidTradeDateError error:
                    return BadRequest(new ApiMessageModel() { Message = result.Error.Message });
                case ApplicationError error:
                    return ControllerExtensions.InternalServerError(this, new ApiMessageModel() { Message = result.Error.Message });
                default:
                    return ControllerExtensions.InternalServerError(this, new ApiMessageModel() { Message = result.Error.Message });
            }
        }



        }
    }