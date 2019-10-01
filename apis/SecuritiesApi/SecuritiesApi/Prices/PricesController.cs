using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Framework.ApiUtil.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Securities.Core.AppInterfaces;
using Securities.Core.Domain;
using Framework.ApiUtil.Controllers;

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
        /// Gets a list closing security price data on a given date.  If no date is privided, then the most recent trade date is used
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetSecurityPrices")]
        public IActionResult Get(DateTime? date)
        {


            var result = _securityPricesService.TryGetSecurityPrices(date, out List<SecurityPrice> securityPrices);

            if (result.IsSuccessful)
            {
                var models = securityPrices.Select(d => new PriceModel()
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
            else if (result.ErrorCode == SecurityPriceErrorCode.TRADE_DATE_NOT_VALID)
            {
                return BadRequest(new ApiMessageModel() { Message = result.ErrorMessage });
            }
            else if ( result.ErrorCode == SecurityPriceErrorCode.NO_DATA_FOR_TICKER_ON_TRADE_DATE)
            {
                return BadRequest(new ApiMessageModel() { Message = result.ErrorMessage });
            }
            else
            {
                return ControllerExtensions.InternalServerError(this, new ApiMessageModel() { Message = "An unknown error occured" });
            }

        }
    }
}