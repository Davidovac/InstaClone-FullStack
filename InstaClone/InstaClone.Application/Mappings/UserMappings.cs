using AutoMapper;
using InstaClone.Application.DTOs.AuthDTOs;
using InstaClone.Application.DTOs.UserDTOs;
using InstaClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<RegisterRequestDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<UserUpdateRequestDto, User>()
                .ForMember(dest => dest.PasswordHash, opt =>
                {
                    opt.Condition(src => src.Password == string.Empty);
                    opt.Ignore();
                });

            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<User, ProfileDto>()
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePicture));

            CreateMap<User, UserSimpleDto>()
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePicture));
        }
    }
}
