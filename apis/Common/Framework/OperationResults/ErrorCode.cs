using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.OperationResults
{
    public class ErrorCode
    {

        protected ErrorCode()
        {

        }

        public int Code { get; private set; }





        public static T New<T>(int code) where T : ErrorCode
        {
            var errorCode = default(T);
            errorCode.Code = code;
            return errorCode;
        }

    }
}
