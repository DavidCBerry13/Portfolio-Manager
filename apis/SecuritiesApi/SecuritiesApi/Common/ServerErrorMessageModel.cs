using DavidBerry.Framework.ApiUtil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritiesApi.Common
{
    public class ServerErrorMessageModel : ApiErrorMessageModel
    {

        public ServerErrorMessageModel()
        {
            Message = "An application error has occurred.  The error has been logged and support teams notified";
            ErrorCode = "SERVER_ERROR";
        }

    }
}
