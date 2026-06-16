using InstaClone.Application.DTOs.CommentDTOs;
using InstaClone.Application.DTOs.LikeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.DTOs.PostDTOs
{
    public class PostSummaryResponseDto
    {
        public Guid Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorPictureUrl { get; set; }
        public string Photo { get; set; }
        public string Caption { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsLiked { get; set; } = false;
        public IReadOnlyList<LikeResponseDto> Likes { get; set; } = new List<LikeResponseDto>();
        public ICollection<CommentResponseDto> Comments { get; set; } = new List<CommentResponseDto>();
    }
}
