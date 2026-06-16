using InstaClone.Application.DTOs.PostDTOs;
using InstaClone.Application.DTOs.UserDTOs;
using InstaClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.DTOs.LikeDTOs
{
    public class LikeResponseDto
    {
        public Guid Id { get; set; }
        public string LikerName { get; set; }
        public string LikerPictureUrl { get; set; } = string.Empty;
        public PostDetailResponseDto Post { get; set; }
    }
}
