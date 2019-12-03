using IdentityCheck.Models;
using IdentityCheck.Models.RequestModels.Account;
using IdentityCheck.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Services
{
    public interface IUserService
    {
        Task<List<string>> LoginAsync(LoginRequest model);
        Task Logout();
        Task<List<string>> RegisterAsync(RegisterRequest model);
        Task<List<Post>> GetPostsAsync(ApplicationUser user);
        Task SaveUserAsync(ApplicationUser user);
        Task<ApplicationUser> GetCurrentUserAsync();

        //Google Auth

        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent);
        Task<List<string>> CreateAndLoginGoogleUser(ExternalLoginInfo info);
    }
}
