using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.Common
{
    public class Result
    {
        public bool IsAuthorized { get; private set; }
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }

        protected Result(bool isSuccess, string errorMessage = null, bool authorized = true)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            IsAuthorized = authorized;
        }

        public static Result Success() => new Result(true);

        public static Result Failure(string errorMessage) => new Result(false, errorMessage);
        public static Result NotAuthorized() => new Result(false,authorized:false);
    }
    public class Result<T> : Result
    {
        public T Data { get; private set; }

        private Result(bool isSuccess, T data, string errorMessage,bool isAuthorized)
            : base(isSuccess, errorMessage,isAuthorized)
        {
            Data = data;
        }

        public static Result<T> Success(T data) => new Result<T>(true, data, null,true);
        public static Result<T> Failure(string errorMessage) => new Result<T>(false, default, errorMessage,true);
        public static Result<T> NotAuthorized() => new Result<T>(false, default, null,false);
    }
}
