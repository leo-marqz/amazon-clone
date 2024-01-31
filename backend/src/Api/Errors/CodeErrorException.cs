using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Api.Errors
{
    public class CodeErrorException : CodeErrorResponse
    {
        [JsonProperty(PropertyName = "details")]
        private string Details {get; set;}

        public CodeErrorException(int statusCode, string[] messages = null, string details = null) 
        : base(statusCode, messages)
        {
            Details = details;
        }
    }
}