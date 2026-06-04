using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Infrastructure.Settings
{
    public class EmailSenderSettings
    {
        public const string SectionName = "EmailSenderSettings";

        public string ApiKey { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
    }
}
