using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Domain.Entities
{
    public class Like
    {
        public Guid Id { get; set; }
        public Guid LikerId { get; set; }
        public User Liker { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
