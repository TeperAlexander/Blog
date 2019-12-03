using AutoMapper;
using IdentityCheck.Models;
using IdentityCheck.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Services.Helpers.AutoMapper.Profiles
{
    public class PostViewModelFromPost :  Profile
    {
        public PostViewModelFromPost()
        {
            CreateMap<Post, PostViewModel>()
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(
                    dest => dest.Author,
                    opt => opt.MapFrom(src => src.Author));
        }
    }
}
