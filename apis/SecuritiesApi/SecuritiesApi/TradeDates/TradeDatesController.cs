using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DavidBerry.Framework.ApiUtil.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Securities.Core.AppInterfaces;

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
            var dates = _tradeDateService.GetTradeDates();

            var models = dates.Select(d => new TradeDateModel()
            {
                TradeDate = d.Date.ToString("yyyy-MM-dd"),
                IsMonthEnd = d.IsMonthEnd,
                IsQuarterEnd = d.IsQuarterEnd,
                IsYearEnd = d.IsYearEnd
            });

            return Ok(models);
        }



        [HttpGet("{date:datetime}", Name = "GetTradeDate")]
        public IActionResult Get(DateTime date)
        {
            var tradeDate = _tradeDateService.GetTradeDate(date.Date);

            if (tradeDate == null)
            {
                return NotFound();
            }
            else
            {
                var model = new TradeDateModel()
                {
                    TradeDate = tradeDate.Date.ToString("yyyy-MM-dd"),
                    IsMonthEnd = tradeDate.IsMonthEnd,
                    IsQuarterEnd = tradeDate.IsQuarterEnd,
                    IsYearEnd = tradeDate.IsYearEnd
                };

                return Ok(model);
            }
        }



        [HttpGet("Latest", Name = "GetLatestTradeDate")]
        public IActionResult GetLatestTradeDate()
        {
            var tradeDate = _tradeDateService.GetLatestTradeDate();

            if (tradeDate == null)
            {
                return NotFound();
            }
            else
            {
                var model = new TradeDateModel()
                {
                    TradeDate = tradeDate.Date.ToString("yyyy-MM-dd"),
                    IsMonthEnd = tradeDate.IsMonthEnd,
                    IsQuarterEnd = tradeDate.IsQuarterEnd,
                    IsYearEnd = tradeDate.IsYearEnd
                };

                return Ok(model);
            }
        }




    }
}