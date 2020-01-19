using DavidBerry.Framework.Functional;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.Errors
{
    public class ApplicationMissingDataError : ApplicationError
    {

        public ApplicationMissingDataError(string message) : base(message)
        {

        }

    }
}
