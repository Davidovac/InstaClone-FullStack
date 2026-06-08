using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.DTOs.AuthDTOs
{
    public class LoginResponseDto
    {
        [Required]
        [MinLength(15)]
        public string Token { get; set; }
    }
}
