using InstaClone.Application.DTOs.PostDTOs;
using InstaClone.Application.DTOs.UserDTOs;
using InstaClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.DTOs.CommentDTOs
{
    public class CommentResponseDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string AuthorName { get; set; }
        public string AuthorPictureUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public IReadOnlyList<ReplyResponseDto> Replies { get; set; } = new List<ReplyResponseDto>();
    }
}
