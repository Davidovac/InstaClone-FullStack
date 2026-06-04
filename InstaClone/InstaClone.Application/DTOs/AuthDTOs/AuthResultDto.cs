using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.DTOs.AuthDTOs
{
    public sealed class AuthResultDto
    {
        public bool Success { get; init; }

        public string? UserId { get; init; }

        public string? Email { get; init; }

        public IReadOnlyList<string>? Roles { get; init; }

        public string? Token { get; init; }

        public IEnumerable<string>? Errors { get; init; }
    }
}
