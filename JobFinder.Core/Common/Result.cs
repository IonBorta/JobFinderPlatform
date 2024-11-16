using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Core.Common
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }

        protected Result(bool isSuccess, string errorMessage = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static Result Success() => new Result(true);

        public static Result Failure(string errorMessage) => new Result(false, errorMessage);
    }
    public class Result<T> : Result
    {
        public T Data { get; private set; }

        private Result(bool isSuccess, T data, string errorMessage)
            : base(isSuccess, errorMessage)
        {
            Data = data;
        }

        public static Result<T> Success(T data) => new Result<T>(true, data, null);
        public static Result<T> Failure(string errorMessage) => new Result<T>(false, default, errorMessage);
    }
}
