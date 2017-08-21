using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ElasticsearchNet.Web
{
    public class GenericErrorHandler : IErrorHandler
    {
        public void Handle(Exception exception, HttpResponse response)
        {
            response.WriteExceptionJson(500, new ExceptionResponse(exception.GetType().Name, exception.Message, exception.StackTrace));
        }
    }

    public interface IErrorHandler
    {
        void Handle(Exception exception, HttpResponse response);
    }

    public class ErrorHandlerOptions
    {
        private readonly Dictionary<Type, IErrorHandler> handlers;

        public ErrorHandlerOptions()
        {
            this.handlers = new Dictionary<Type, IErrorHandler>();
        }

        public void AddHandler<T>(IErrorHandler handler) where T : Exception
        {
            this.handlers.Add(typeof(T), handler);
        }

        public ReadOnlyDictionary<Type, IErrorHandler> Handlers => new ReadOnlyDictionary<Type, IErrorHandler>(handlers);
    }

    public static class ResponseExtensions
    {
        public static void WriteExceptionJson(this HttpResponse res, int statusCode, ExceptionResponse exRes)
        {
            res.StatusCode = statusCode;
            res.ContentType = "application/json";

            var byteString = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(exRes));

            res.Body.Write(byteString, 0, byteString.Length);
        }
    }
}
