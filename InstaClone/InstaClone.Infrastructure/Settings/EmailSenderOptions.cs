using InstaClone.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Infrastructure.Settings
{
    public class EmailSenderOptions : IEmailSenderOptions
    {
        public string ApiKey { get; set; } = string.Empty;
    }
}
