using InstaClone.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Infrastructure.Settings
{
    public class ServedAppSettings : IServedAppSettings
    {
        public string DefaultConnectionString { get; set; }
        public string FrontendBaseUrl { get; set; }
        public string JWTIssuer { get; set; }
        public string JWTAudience { get; set; }
        public string JWTSecret { get; set; }
        public string EmailSenderApiKey { get; set; }
    }
}
