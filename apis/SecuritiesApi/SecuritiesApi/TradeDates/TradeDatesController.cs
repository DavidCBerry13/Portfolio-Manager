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
        [HttpGet(Name = "GetTradeDates")]
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
                return ControllerExtensions.InternalServerError(this, new ApiMessageModel() { Message = result.Error.Message });
            }
        }



        [HttpGet("{date:datetime}", Name = "GetTradeDate")]
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
                return BadRequest(new ApiMessageModel() { Message = result.Error.Message });
            }
        }



        [HttpGet("Latest", Name = "GetLatestTradeDate")]
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
                return ControllerExtensions.InternalServerError(this, new ApiMessageModel() { Message = result.Error.Message });
            }

        }




    }
}