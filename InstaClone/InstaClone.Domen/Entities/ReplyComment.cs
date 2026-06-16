using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Domain.Entities
{
    public class ReplyComment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid RepliedCommentId { get; set; }
        public Comment RepliedComment { get; set; }
        public Guid AuthorId { get; set; }
        public User Author { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
