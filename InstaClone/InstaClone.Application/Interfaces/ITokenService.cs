using InstaClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user, List<string> roles);
    }
}
