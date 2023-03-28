﻿using System.Net;

namespace BubberDinner.Application.Common.Errors
{
    public interface IError
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorMessage { get; }
    }
}