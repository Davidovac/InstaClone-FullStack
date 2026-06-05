using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.DTOs.AuthDTOs
{
    public class ActivateAccountRequestDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
