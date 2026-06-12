using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Domain.Entities
{
    public class Comment
    {
        public Guid Id {  get; set; }
        public string Text { get; set; }
        public Guid AuthorId { get; set; }
        public User Author { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<ReplyComment> ReplyComments { get; set; } = new List<ReplyComment>();
    }
}
