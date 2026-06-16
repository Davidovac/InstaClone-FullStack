using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.DTOs.UserDTOs
{
    public class ProfileDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public string ProfileDesc { get; set; } = string.Empty;
        public int PostsCount { get; set; } = 0;
        public int FollowerCount { get; set; } = 0;
        public int FollowingCount { get; set; } = 0;
    }
}
