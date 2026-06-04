using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.Interfaces
{
    public interface IServedAppSettings
    {
        public string DefaultConnectionString { get; }
        public string FrontendBaseUrl { get; }
        public string JWTIssuer { get; }
        public string JWTAudience { get; }
        public string JWTSecret { get; }
        public string EmailSenderApiKey { get; }
    }
}
