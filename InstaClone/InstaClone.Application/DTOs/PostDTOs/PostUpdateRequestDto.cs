using InstaClone.Application.DTOs.CommentDTOs;
using InstaClone.Application.DTOs.LikeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.DTOs.PostDTOs
{
    public class PostUpdateRequestDto
    {
        public Guid Id { get; set; }
        public string Photo { get; set; }
        public string Caption { get; set; }
    }
}
