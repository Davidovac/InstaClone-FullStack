using AutoMapper;
using InstaClone.Application.DTOs.LikeDTOs;
using InstaClone.Application.DTOs.PostDTOs;
using InstaClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.Mappings
{
    public class PostMappings : Profile
    {
        public PostMappings()
        {
            CreateMap<PostCreateRequestDto, Post>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<PostUpdateRequestDto, Post>();

            CreateMap<Post, PostSummaryResponseDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.UserName))
                .ForMember(dest => dest.AuthorPictureUrl, opt => opt.MapFrom(src => src.Author.ProfilePicture));

            CreateMap<Post, PostDetailResponseDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.UserName))
                .ForMember(dest => dest.AuthorPictureUrl, opt => opt.MapFrom(src => src.Author.ProfilePicture));


            //Likes simple

            CreateMap<LikeRequestDto, Like>();

            CreateMap<Like, LikeResponseDto>()
                .ForMember(dest => dest.LikerName, opt => opt.MapFrom(src => src.Liker.UserName))
                .ForMember(dest => dest.LikerPictureUrl, opt => opt.MapFrom(src => src.Liker.ProfilePicture)); ;
        }
    }
}
