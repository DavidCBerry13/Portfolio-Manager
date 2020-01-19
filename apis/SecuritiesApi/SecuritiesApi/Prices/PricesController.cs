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
using DavidBerry.Framework.Util;
using System.Text.RegularExpressions;
using Securities.Core.Errors;
using SecuritiesApi.Common;

namespace SecuritiesApi.Prices
{


    /// <summary>
    /// Endpoint for getting prices for multiple securities on a given trade date
    /// </summary>
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
        /// Gets a the closing security price data on a given date for all securities.  If no date is provided, then the most recent trade date is used
        /// </summary>
        /// <param name="date">The date to get prices for.  If no date is provided, the latest date is used</param>
        /// <param name="tickers">An optional comma separated list of up to 500 ticker symbols to get the prices for.  If no list is provided then all securities will be returned</param>
        /// <returns></returns>
        [HttpGet(Name = "GetSecurityPrices")]
        public IActionResult Get(String tickers, DateTime? date)
        {
            return (String.IsNullOrWhiteSpace(tickers) ) ?
                GetPricesForAllSecurities(date)
                : GetPricesForSecuritiesList(date, tickers);
        }


        [NonAction]
        protected IActionResult GetPricesForAllSecurities(DateTime? date)
        {
            var result = _securityPricesService.GetSecurityPrices(date);

            if (result.IsSuccess)
            {
                var models = _mapper.Map<List<SecurityPrice>, List<PriceModel>>(result.Value);

                return Ok(models);
            }
            else
            {
                switch (result.Error)
                {
                    case InvalidTradeDateError error:
                        return BadRequest(new InvalidTradeDateMessageModel(error));
                    case ApplicationMissingDataError error:
                        return ControllerExtensions.InternalServerError(this, new ServerErrorMessageModel());
                    default:
                        return ControllerExtensions.InternalServerError(this, new ServerErrorMessageModel());
                }
            }
        }


        [NonAction]
        protected IActionResult GetPricesForSecuritiesList(DateTime? date, String tickers)
        {
            if (!Regex.IsMatch(tickers, "^([A-Z]{1,5})(,([A-Z]{1,5})){0,499}$"))
                return BadRequest("If a list of ticker symbols is provided, it must be as a comma separated list (without spaces)");

            var tickerList = tickers.Split(',');
            var result = _securityPricesService.GetSecurityPrices(date, tickerList);

            if (result.IsSuccess)
            {
                var models = _mapper.Map<List<SecurityPrice>, List<PriceModel>>(result.Value);

                return Ok(models);
            }
            else
            {
                switch (result.Error)
                {
                    case InvalidTradeDateError error:
                        return BadRequest(new InvalidTradeDateMessageModel(error));
                    case TickerNotFoundError error:
                        return BadRequest(new TickerNotFoundMessageModel(error));
                    case InvalidDateForTickerError error:
                        return BadRequest(new InvalidDateForTickerMessageModel(error));
                    default:
                        return ControllerExtensions.InternalServerError(this, new ServerErrorMessageModel());
                }
            }
        }



        //[HttpGet("{ticker:alpha:length(1,5)}", Name = "GetSecurityPrice")]
        //public IActionResult Get(string ticker, DateTime? date)
        //{
        //    var result = _securityPricesService.GetSecurityPrice(ticker, date);

        //    if (result.IsSuccess)
        //    {
        //        var price = result.Value;
        //        var model = new PriceModel()
        //        {
        //            Ticker = price.Ticker,
        //            TradeDate = price.TradeDate.ToString("yyyy-MM-dd"),
        //            Name = price.Name,
        //            SecurityType = price.SecurityType,
        //            OpeningPrice = price.OpeningPrice,
        //            ClosingPrice = price.ClosingPrice,
        //            DailyHigh = price.DailyHigh,
        //            DailyLow = price.DailyLow,
        //            Volume = price.Volume,
        //            Change = price.Change,
        //            ChangePercent = price.ChangePercent
        //        };

        //        return Ok(model);
        //    }
        //    else
        //    {
        //        return NotFound(new ApiMessageModel() { Message = result.Error.Message });
        //    }

        //}

    }
}