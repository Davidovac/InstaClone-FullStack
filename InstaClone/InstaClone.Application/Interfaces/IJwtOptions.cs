using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.Interfaces
{
    public interface IJwtOptions
    {
        public string Secret { get; }
        public string Issuer { get; }
        public string Audience { get; }
    }
}
