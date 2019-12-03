using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IdentityCheck.Models;
using IdentityCheck.Services.User;
using IdentityCheck.Services;
using Microsoft.AspNetCore.Authorization;
using IdentityCheck.Models.ViewModels;
using ReflectionIT.Mvc.Paging;
using IdentityCheck.Utils;

namespace IdentityCheck.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IPostService postService;

        public HomeController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IPostService postService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.postService = postService;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Redirect()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        [Authorize]
        [HttpGet("/home")]
        public async Task<IActionResult> Index(QueryParams queryParams)
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var posts = await postService.GetPostsByParamsAsync(queryParams, currentUser);
            return View(new IndexViewModel
            {
                AppUser = currentUser,
                PagingList = posts,
                QueryParams = queryParams,
                ActionName = nameof(Index)
            });
        }
    }
}
