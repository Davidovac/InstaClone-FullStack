using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InstaClone.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public User Author {  get; set; }
        public string Photo {  get; set; } = string.Empty;
        public string Caption { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public void LikePost(User liker)
        {
            if (liker == null)
                throw new ArgumentNullException();

            var like = Likes.Where(l => l.Liker == liker).First();

            if (like == null)
            {
                Like newLike = new Like
                {
                    Liker = liker,
                    Post = this,
                };

                Likes.Add(newLike);
            }
        }

        public void UnlikePost(User unliker)
        {
            if (unliker == null)
                throw new ArgumentNullException();

            var like = Likes.Where(l => l.Liker == unliker).First();

            if (like != null)
            {
                Likes.Remove(like);
            }
        }
    }
}
