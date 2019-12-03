using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Services.Helpers.AutoMapper.Profiles
{
    public static class AutoMapperSetup
    {
        public static void SetUpAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PostFromPostRequestProfile());
                cfg.AddProfile(new PostRequestFromUserProfile());
                cfg.AddProfile(new PostRequestFromPostProfile());
                cfg.AddProfile(new PostViewModelFromPost());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
           
        }
    }
}
