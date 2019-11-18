using Framework.ApiUtil.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ApiUtil.Controllers
{
    public static class ControllerExtensions
    {

        /// <summary>
        /// Returns a forbidden (HTTP 403) result with no additional data
        /// </summary>
        /// <param name="controller">A Controller object this method is being called against</param>
        /// <returns>A ForbiddenResult object representing an HTTP 403 response</returns>
        public static ActionResult Forbidden(this Controller controller)
        {
            return new ForbiddenResult();
        }

        /// <summary>
        /// Returns a forbidden (HTTP 403) result with the speccifed value object included in the response
        /// </summary>
        /// <param name="controller">A Controller object this method is being called against</param>
        /// <param name="value">A FobiddenObjectResult object representing an HTTP 403 response with a payload describing 
        /// why the request was rejected</param>
        /// <returns></returns>
        public static ActionResult Forbidden(this ApiControllerBase controller, object value)
        {
            return new ForbiddenObjectResult(value);
        }


        public static ActionResult InternalServerError(this ApiControllerBase controller)
        {
            return new InternalServerErrorResult();
        }

        public static ActionResult InternalServerError(this ApiControllerBase controller, object value)
        {
            return new InternalServerErrorObjectResult(value);
        }
    }
}
