using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.DTOs.AuthDTOs
{
    public class RegisterRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Role { get; set; } = null;

        public bool ValidatePassword()
        {
            if (this.Password.Length < 8)
                return false;
            if (!this.Password.Any(char.IsUpper))
                return false;
            if (!this.Password.Any(char.IsLower))
                return false;
            if (!this.Password.Any(char.IsDigit))
                return false;
            if (!this.Password.Any(ch => !char.IsLetterOrDigit(ch)))
                return false;
            return true;
        }
    }

}
