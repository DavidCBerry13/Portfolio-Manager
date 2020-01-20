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
using Securities.Core.Domain;
using Securities.Core.Errors;
using SecuritiesApi.Common;
using SecuritiesApi.Securities.Prices.Models;

namespace SecuritiesApi.Securities.Prices
{
    [Produces("application/json")]
    [Route("api/Securities/{ticker}/Prices")]
    [ApiController]
    public class SecurityPricesController : ApiControllerBase
    {


        public SecurityPricesController(ILogger<SecurityPricesController> logger, IMapper mapper, ISecurityPriceService securityPricesService)
            : base(logger, mapper)
        {
            _securityPricesService = securityPricesService;
        }

        private readonly ISecurityPriceService _securityPricesService;


        [HttpGet(Name = "GetPricesForSecurity")]
        public IActionResult Get(String ticker, [FromQuery]SecurityPriceParameters parameters)
        {
            var result = _securityPricesService.GetSecurityPrices(ticker, parameters.StartDate, parameters.EndDate);
            if (result.IsSuccess)
            {
                var models = _mapper.Map<List<SecurityPrice>, List<PriceModel>>(result.Value);
                return Ok(models);
            }
            else
            {
                switch (result.Error)
                {
                    case InvalidTickerFormatError error:
                        return BadRequest(new InvalidTickerFormatMessageModel(error));
                    case TickerNotFoundError error:
                        return BadRequest(new TickerNotFoundMessageModel(error));
                    case ApplicationMissingDataError error:
                        return ControllerExtensions.InternalServerError(this, new ServerErrorMessageModel());
                    default:
                        return ControllerExtensions.InternalServerError(this, new ServerErrorMessageModel());
                }
            }
        }

    }
}