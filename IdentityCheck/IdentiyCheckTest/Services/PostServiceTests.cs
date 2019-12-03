using AutoMapper;
using IdentityCheck.Data;
using IdentityCheck.Models;
using IdentityCheck.Models.RequestModels;
using IdentityCheck.Services;
using IdentiyCheckTest.TestUtils;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace IdentiyCheckTest.Services
{
    [Collection("Database collection")]
    public class PostServiceTests
    {
        private readonly DbContextOptions<ApplicationDbContext> options;
        private readonly Mock<IMapper> mockMapper;

        public PostServiceTests()
        {
            options = TestDbOptions.Get();
            mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Post_Is_Added_When_Correct()
        {
            using (var context = new ApplicationDbContext(options))
            {
                var postRequest = new PostRequest()
                {
                    Title = "Test"
                };

                mockMapper.Setup(x => x.Map<PostRequest, Post>(It.IsAny<PostRequest>(), It.IsAny<Post>()))
                .Returns(new Post()
                {
                    Title = postRequest.Title
                });

                var postService = new PostService(context, mockMapper.Object);
                var length = await context.Posts.CountAsync();
                var post = await postService.SaveAsync(postRequest);

                Assert.Equal(length + 1, await context.Posts.CountAsync());
                Assert.Equal(post.Title, postRequest.Title);
            }
        }      
    }
}
