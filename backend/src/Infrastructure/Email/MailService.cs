

using System;
using System.Threading.Tasks;
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.Email;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Email
{
    public class MailService : IMailService
    {
        private readonly MailSettings _settings;
        private readonly ILogger<MailService> _logger;
        private MailjetClient _client;
        
        public MailService(IOptions<MailSettings> mailSettings, ILogger<MailService> logger)
        {
            _settings = mailSettings.Value;
            _logger = logger;

            _client = new MailjetClient(_settings.APIKey, _settings.SecretKey){
                Version = ApiVersion.V3_1
            };
        }

        public async Task<bool> MailResetPasswordAsync(MailContent content, string token = null)
        {
            try
            {
                if(string.IsNullOrEmpty(token))return false;
                if(content is null) return false;

                var request = new MailjetRequest
                {
                    Resource = Send.Resource
                };

                var btnStyles = "display:block;padding:5px;margin-top:15px;backgroud:blue;color:white;";
                var btnResetPassword = $"<a href='{_settings.UrlBase}/password/reset/{token}' class='{btnStyles}' >Reset Password</a>";

                request.Property(Send.Messages, new JArray {
                    new JObject {
                        {"From", new JObject {
                                {"Email", _settings.MailBase },
                                {"Name", _settings.ApplicationName}
                            }
                        },
                        {"To", new JArray {
                                new JObject {
                                    {"Email", content.To},
                                    {"Name", content.ToName }
                                }
                            }
                        },
                        {"Subject", content.Subject},
                        {"HTMLPart", $"<div>" +
                                     $"<p>{content.Body}</p>" +
                                     $"<div>{btnResetPassword}</div>" +
                                     "</div>"}
                    }
                });
                
                var response = await _client.PostAsync(request);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error, el mail para reset passowrd no fue enviado: \n " + ex.Message);
                return false;
            }
        }

        public async Task<bool> RunAsync(MailContent content, string token = null)
        {
            try{

                if(string.IsNullOrEmpty(token))return false;
                if(content is null) return false;

                var request = new MailjetRequest
                {
                    Resource = Send.Resource
                }
                .Property(Send.Messages, new JArray {
                    new JObject {
                        {"From", new JObject {
                                {"Email", content.From },
                                {"Name", content.FromName}
                            }
                        },
                        {"To", new JArray {
                                new JObject {
                                    {"Email", content.To},
                                    {"Name", content.ToName }
                                }
                            }
                        },
                        {"Subject", content.Subject},
                        {"HTMLPart", content.Body}
                    }
                });
                
                var response = await _client.PostAsync(request);

                return response.IsSuccessStatusCode;

            }catch(Exception ex){
                _logger.LogError("Error, el mail no fue enviado: \n " + ex.Message);
                return false;
            }
        }

        public Task<bool> RunAsync(MailContent content)
        {
            throw new NotImplementedException();
        }
    }
}