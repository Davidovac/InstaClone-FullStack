using AutoMapper;
using InstaClone.Application.DTOs.CommentDTOs;
using InstaClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.Mappings
{
    public class CommentMappings : Profile
    {
        public CommentMappings()
        {
            //COMMENTS (on posts)

            CreateMap<CommentCreateRequestDto, Comment>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<CommentUpdateRequestDto, Comment>();

            CreateMap<Comment, CommentResponseDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.UserName))
                .ForMember(dest => dest.AuthorPictureUrl, opt => opt.MapFrom(src => src.Author.ProfilePicture));

            //REPLIES (on post comments)

            CreateMap<ReplyCreateRequestDto, Comment>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<ReplyComment, ReplyResponseDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.UserName))
                .ForMember(dest => dest.AuthorPictureUrl, opt => opt.MapFrom(src => src.Author.ProfilePicture));
        }
    }
}
