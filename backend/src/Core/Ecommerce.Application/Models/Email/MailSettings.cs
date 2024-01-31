using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Models.Email
{
    public class MailSettings
    {
        public string APIKey { get; set; }
        public string SecretKey { get; set; }
        public string MailBase { get; set; }
        public string UrlBase { get; set; }
        public string ApplicationName { get; set; }
    }
}