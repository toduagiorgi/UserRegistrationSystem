using System;
using System.Collections.Generic;
using System.Text;

namespace UserRegistrationSystem.Core.Models.Response
{
    public class Response<T>
    {
        public Response()
        {
            Code = 0;
            Message = string.Empty;
            Succeed = true;
        }

        public Response(T obj) => Data = obj;

        public int Code { get; set; }
        public string Message { get; set; }
        public bool Succeed { get; set; }
        public T Data { get; set; }

        public void SetError(int code, string message)
        {
            Code = code;
            Message = message;
            Succeed = false;
        }
    }

    public abstract class IResponse
    {
        private IResponse()
        {

        }

        //public sealed class Success<T> : IResponse
        //{
        //    public Success(T data)
        //    {
        //        Data = data;
        //    }
        //    public T Data { get; set; }
        //    public string Message { get; set; }
        //}

        public sealed class Success<T> : IResponse
        {
            public Success(string message, T data)
            {
                Message = message;
                Data = data;
            }
            public string Message { get; set; }
            public T Data { get; set; }
        }

        public sealed class Failed : IResponse
        {
            public Failed(int errorCode, string errorMessage)
            {
                ErrorCode = errorCode;
                ErrorMessage = errorMessage;
            }
            public int ErrorCode { get; set; }
            public string ErrorMessage { get; set; }
        }        

        public sealed class NotFound : IResponse
        {
            public NotFound(string message)
            {
                Message = message;
            }
            public string Message { get; set; }
        }
    }
}
