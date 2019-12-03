using AutoMapper;
using IdentityCheck.Models;
using IdentityCheck.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Services.Helpers.AutoMapper.Profiles
{
    public class PostFromPostRequestProfile : Profile
    {
        public PostFromPostRequestProfile()
        {
            CreateMap<PostRequest, Post>()
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(
                    dest => dest.Author,
                    opt => opt.MapFrom(src => src.Author))
                .ForMember(
                    dest => dest.AuthorId,
                    opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(
                    dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
