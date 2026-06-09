using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.DTOs.UserDTOs
{
    public class UserUpdateRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
