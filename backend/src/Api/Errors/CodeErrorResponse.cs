using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Api.Errors
{
    public class CodeErrorResponse
    {
        [JsonProperty(PropertyName = "statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty(PropertyName = "messages")]
        public string[] Messages { get; set; }

        public CodeErrorResponse(int statusCode, string[] messages = null)
        {
            StatusCode = statusCode;
            if(messages is null){
                Messages = new string[0];
                var txt = GetDefaultMessageStatusCode(statusCode);
                Messages[0] = txt;
            }else{
                Messages = messages;
            }
        }

        private string GetDefaultMessageStatusCode(int statusCode){
            return statusCode switch {
                400 => "El request tiene errores",
                401 => "No tienes autorizacion para este recurso",
                404 => "No se encontro el recurso",
                500 => "Se produjeron errores en el servidor",
                _ => string.Empty
            };
        }
    }
}