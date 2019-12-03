using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityCheck.Data;
using IdentityCheck.Models;
using IdentityCheck.Models.RequestModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace IdentityCheck.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IStringLocalizer<UserService> localizer;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            , ApplicationDbContext applicationDbContext, IStringLocalizer<UserService> localizer)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.applicationDbContext = applicationDbContext;
            this.localizer = localizer;
        }

        public async Task<List<Post>> GetPostsAsync(ApplicationUser user)
        {
            var cuser = await applicationDbContext.Users.Include(u => u.Posts).FirstAsync(u => u.Email == user.Email);
            return cuser.Posts.ToList();
        }

        public async Task<List<string>> LoginAsync(LoginRequest model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                model.ErrorMessages.Add(localizer["User with the given Email does not exist."]);
                return model.ErrorMessages;
            }
            var result = await signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);
            model.ErrorMessages = checkLoginErrors(result, model.ErrorMessages);
            return model.ErrorMessages;
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<List<string>> RegisterAsync(RegisterRequest model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "RegularUser");
                user.TimeZoneId = TimeZoneInfo.Local.Id;
                await applicationDbContext.SaveChangesAsync();
                await signInManager.SignInAsync(user, isPersistent: false);
            }
            return result.Errors
                .Select(e => e.Description)
                .ToList();
        }

        public async Task SaveUserAsync(ApplicationUser user)
        {
            applicationDbContext.Attach(user);
            await applicationDbContext.SaveChangesAsync();

        }

        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var claimsPrincipal = signInManager.Context.User;

            if (claimsPrincipal == null)
            {
                return null;
            }
            var user = await userManager.GetUserAsync(claimsPrincipal);
            return user;
        }

        private List<string> checkLoginErrors(SignInResult result, List<string> errors)
        {
            if (result.IsLockedOut)
            {
                errors.Add(localizer["User account locked out."]);
            }
            if (result.IsNotAllowed)
            {
                errors.Add(localizer["User is not allowed to login."]);
            }
            if (result.RequiresTwoFactor)
            {
                errors.Add(localizer["Two factor authentication is required."]);
            }
            if (!result.Succeeded)
            {
                errors.Add(localizer["Invalid login attempt."]);
            }
            return errors;
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        {
            return signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent)
        {
            return await signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent);
        }

        public async Task<List<string>> CreateAndLoginGoogleUser(ExternalLoginInfo info)
        {
            var user = new ApplicationUser
            {
                Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
            };

            if (user.TimeZoneId == null)
            {
                user.TimeZoneId = TimeZoneInfo.Local.Id;
                await applicationDbContext.SaveChangesAsync();
            }

            var identResult = await userManager.CreateAsync(user);
            if (identResult.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "RegularUser");
                identResult = await userManager.AddLoginAsync(user, info);

                if (identResult.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                }
            }
            return identResult.Errors
                .Select(e => e.Description)
                .ToList();
        }

    }
}
