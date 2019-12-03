using AutoMapper;
using IdentityCheck.Models;
using IdentityCheck.Models.RequestModels;
using IdentityCheck.Models.ViewModels;
using IdentityCheck.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public PostController(IPostService postService, IMapper mapper, 
            IImageService imageService, IUserService userService)
        {
            this.postService = postService;
            this.imageService = imageService;
            this.mapper = mapper;
            this.userService = userService;
        }

        [HttpGet("/add")]
        public IActionResult Add()
        {
            return View(new PostRequest());
        }

        [HttpPost("/add")]
        public async Task<IActionResult> Add(PostRequest postRequest)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userService.GetCurrentUserAsync();
                postRequest.Author = currentUser;
                postRequest.AuthorId = currentUser.Id;
                await postService.SaveAsync(postRequest);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(postRequest);
        }


        [HttpPost("/delete")]
        public async Task<IActionResult> Delete(long id)
        {
            await postService.DeleteAsync(id);
            await imageService.DeleteAllFileAsync(id);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Authorize(Roles = "SuperUser")]
        [HttpGet("/edit/{postId}")]
        public async Task<IActionResult> Edit(long postId)
        {
            var post = await postService.FindByIdAsync(postId);
            var request = mapper.Map<Post, PostRequest>(post);
            return View(request);
        }

        [Authorize(Roles = "SuperUser")]
        [HttpPost("/edit/{id}")]
        public async Task<IActionResult> Edit(PostRequest postRequest, long id)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await userService.GetCurrentUserAsync();
                postRequest = mapper.Map<ApplicationUser, PostRequest>(currentUser, postRequest);
                await postService.EditAsync(id, postRequest);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(postRequest);
        }

        [Authorize(Roles = "SuperUser")]
        [HttpGet("/addimage/{postid}")]
        public IActionResult AddImage(long postId)
        {
            var imageViewModel = new ImageViewModel()
            {
                PostId = postId
            };
            return View(imageViewModel);
        }

        [HttpPost("/addimage/{postid}")]
        public async Task<IActionResult> AddImage(ImageViewModel imageList)
        {
            imageList.WrongFiles = await imageService.UploadImagesAsync(imageList.Images, imageList.PostId);
            if(imageList.WrongFiles.Count == 0)
            {
                return RedirectToAction(nameof(Post), imageList.PostId);
            }
            return View(imageList);
        }

        [HttpGet("/post/{postId}")]
        public async Task<IActionResult> Post(long postId)
        {
            var post = await postService.FindByIdAsync(postId);
            var viewModel = mapper.Map<Post, PostViewModel>(post);
            viewModel.Images = await imageService.GetImageListAsync(postId);
            return View(viewModel);
        }
    }
}
