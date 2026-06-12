using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; } = string.Empty;
        public string ProfileDesc { get; set; } = string.Empty;
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<User> Followers { get; set; } = new List<User>();
        public ICollection<User> Following {  get; set; } = new List<User>();
    }
}
