using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.DTOs.AuthDTOs
{
    public class RegisterRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
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
