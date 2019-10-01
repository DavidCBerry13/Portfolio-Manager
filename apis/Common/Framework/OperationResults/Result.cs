using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.OperationResults
{
    public class Result<TErrorCode>  where TErrorCode : Framework.OperationResults.ErrorCode
    {

        protected Result(bool successful, TErrorCode errorCode, String message)
        {
            IsSuccessful = successful;
            ErrorCode = errorCode;
            ErrorMessage = message;
        }


        public bool IsSuccessful { get; private set; }


        public TErrorCode ErrorCode { get; private set; }


        public string ErrorMessage { get; private set; }



        public static Result<TErrorCode> Success()
        {
            return new Result<TErrorCode>(true, null, String.Empty);
        }


        public static Result<TErrorCode> Error(TErrorCode errorCode, String errorMessage)
        {
            return new Result<TErrorCode>(true, errorCode, errorMessage);
        }

    }



}
