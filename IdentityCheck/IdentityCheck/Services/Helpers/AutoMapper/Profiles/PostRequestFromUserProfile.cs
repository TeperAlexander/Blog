using AutoMapper;
using IdentityCheck.Models;
using IdentityCheck.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Services.Helpers.AutoMapper.Profiles
{
    public class PostRequestFromUserProfile : Profile
    {
        public PostRequestFromUserProfile()
        {
            CreateMap<ApplicationUser, PostRequest>()
                .ForMember(
                    dest => dest.Author,
                    opt => opt.MapFrom(src => src))
                .ForMember(
                    dest => dest.AuthorId,
                    opt => opt.MapFrom(src => src.Id));
        }
    }
}
