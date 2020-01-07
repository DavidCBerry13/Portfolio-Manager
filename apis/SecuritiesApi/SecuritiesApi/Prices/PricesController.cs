using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DavidBerry.Framework.ApiUtil.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Securities.Core.AppInterfaces;
using Securities.Core.Domain;
using DavidBerry.Framework.ApiUtil.Controllers;

namespace SecuritiesApi.Prices
{
    [Route("api/[controller]")]
    public class PricesController : ApiControllerBase
    {

        public PricesController(ILogger<PricesController> logger, IMapper mapper, ISecurityPriceService securityPricesService)
            : base(logger, mapper)
        {
            _securityPricesService = securityPricesService;
        }


        private readonly ISecurityPriceService _securityPricesService;


        /// <summary>
        /// Gets a list closing security price data on a given date.  If no date is provided, then the most recent trade date is used
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetSecurityPrices")]
        public IActionResult Get(DateTime? date)
        {
            var result = _securityPricesService.TryGetSecurityPrices(date);

            if (result.IsSuccess)
            {
                var models = result.Value.Select(d => new PriceModel()
                {
                    Ticker = d.Ticker,
                    TradeDate = d.TradeDate.ToString("yyyy-MM-dd"),
                    Name = d.Name,
                    SecurityType = d.SecurityType,
                    OpeningPrice = d.OpeningPrice,
                    ClosingPrice = d.ClosingPrice,
                    DailyHigh = d.DailyHigh,
                    DailyLow = d.DailyLow,
                    Volume = d.Volume,
                    Change = d.Change,
                    ChangePercent = d.ChangePercent
                });

                return Ok(models);
            }
            else
            {
                return NotFound(new ApiMessageModel() { Message = result.Error.Message });
            }


        }
    }
}