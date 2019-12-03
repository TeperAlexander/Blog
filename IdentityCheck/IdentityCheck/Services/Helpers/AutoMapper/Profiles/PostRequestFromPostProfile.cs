using AutoMapper;
using IdentityCheck.Models;
using IdentityCheck.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Services.Helpers.AutoMapper.Profiles
{
    public class PostRequestFromPostProfile : Profile
    {
        public PostRequestFromPostProfile()
        {
            CreateMap<Post, PostRequest>();
        }
    }
}
