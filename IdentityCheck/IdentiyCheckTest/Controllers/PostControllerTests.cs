using AutoMapper;
using IdentityCheck.Models;
using IdentityCheck.Services;
using Moq;
using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using IdentityCheck.Controllers;
using IdentityCheck.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using IdentityCheck.Utils;
using ReflectionIT.Mvc.Paging;

namespace IdentiyCheckTest.Controllers
{

    public class PostControllerTests
    {
        private readonly Mock<IPostService> mockPostService;
        private readonly Mock<IImageService> mockImageService;
        private readonly Mock<IUserService> mockUserService;
        private readonly Mock<IMapper> mockMapper;

        public PostControllerTests()
        {
            mockPostService = new Mock<IPostService>();
            mockUserService = new Mock<IUserService>();
            mockImageService = new Mock<IImageService>();
            mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Add_Post_Should_Call_Service_And_Redirect()
        {
            var user = new ApplicationUser()
            {
                Id = "test"
            };
            
            mockUserService.Setup(x => x.GetCurrentUserAsync())
                .Returns(Task.FromResult(user));
                
            var controller = new PostController(mockPostService.Object, mockMapper.Object,
                mockImageService.Object, mockUserService.Object);

            var postRequest = new PostRequest()
            {
                Title = "Test"
            };

            var result = await controller.Add(postRequest);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);        
            mockPostService.Verify(s => s.SaveAsync(postRequest), Times.Once);
            Assert.Equal("Index" , redirectResult.ActionName); 
        }
    }
}
