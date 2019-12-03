using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityCheck.Data;
using IdentityCheck.Models;
using IdentityCheck.Models.RequestModels;
using IdentityCheck.Utils;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace IdentityCheck.Services
{
    public class PostService : IPostService
    {
        private const int PageSize = 5;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public PostService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        public async Task DeleteAsync(long id)
        {
            var post = await FindByIdAsync(id);
            applicationDbContext.Posts.Remove(post);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<Post> EditAsync(long id, PostRequest request)
        {
            var post = await FindByIdAsync(id);
            post = mapper.Map<PostRequest, Post>(request, post);
            await applicationDbContext.SaveChangesAsync();
            return post;
        }

        public async Task<Post> FindByIdAsync(long postId)
        {
            return await applicationDbContext.Posts.Include(p => p.Author).FirstOrDefaultAsync(p => p.PostId == postId);
        }

        public async Task<PagingList<Post>> GetPostsByParamsAsync(QueryParams queryParams, ApplicationUser user)
        {
            var posts = await applicationDbContext.Posts.Include(p => p.Author)
               .Where(p => p.Title.Contains(queryParams.Title) || String.IsNullOrEmpty(queryParams.Title))
               .Where(p => p.Description.Contains(queryParams.Description) || String.IsNullOrEmpty(queryParams.Description))
               .Where(p => p.Author.Email == user.Email)
               .ToListAsync();
            return PagingList.Create(posts, PageSize, queryParams.Page);
        }

        public async Task<Post> SaveAsync(PostRequest postRequest)
        {
            var tempPost = new Post();
            var post = mapper.Map<PostRequest, Post>(postRequest, tempPost);
            await applicationDbContext.Posts.AddAsync(post);
            await applicationDbContext.SaveChangesAsync();
            return post;
        }
    }
}
