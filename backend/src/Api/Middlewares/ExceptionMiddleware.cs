using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Errors;
using Ecommerce.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;   
        }

        public async Task InvokeAsync(HttpContext context){
            try
            {
                await _next(context);
            }
            catch ( Exception e)
            {
                _logger.LogError(e, e.Message);
                context.Response.ContentType = "application/json";
                var statusCode = (int) HttpStatusCode.InternalServerError;

                var result = string.Empty;

                switch(e){
                    case NotFoundException notFoundException:
                        statusCode = (int) HttpStatusCode.NotFound;
                        break;
                    case FluentValidation.ValidationException validationException:
                        statusCode = (int) HttpStatusCode.BadRequest;
                        var errors = validationException.Errors.Select(ers=>ers.ErrorMessage).ToArray();
                        var validationjson = JsonConvert.SerializeObject(errors);
                        result = JsonConvert.SerializeObject(
                            new CodeErrorException(statusCode, errors, validationjson)
                            );
                        break;
                    case BadRequestException badRequestException:
                        statusCode = (int) HttpStatusCode.BadRequest;
                        break;
                    default:
                        statusCode = (int) HttpStatusCode.InternalServerError;
                    break;
                }

                if(string.IsNullOrEmpty(result)){
                    result = JsonConvert.SerializeObject(
                        new CodeErrorException(statusCode, new string[]{e.Message, e.StackTrace}));
                }

                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(result);
            }
        }
    }
}