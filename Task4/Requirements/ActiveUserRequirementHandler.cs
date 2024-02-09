using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Task4.Models;

namespace Task4.Auth
{
    public class ActiveUserRequirementHandler : AuthorizationHandler<ActiveUserRequirement>
    {
        private readonly UserManager<UserProfile> _userManager;
        private readonly SignInManager<UserProfile> _signInManager;

        // UserManager<TUser> available via dependency injection. U can inject anything you want
        public ActiveUserRequirementHandler(UserManager<UserProfile> userManager, SignInManager<UserProfile> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        /// <inheritdoc />
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ActiveUserRequirement requirement)
        {
            var userFromDb = await _userManager.GetUserAsync(context.User);

            // Check whatever qualifies as a deleted user in your usecase.

            if (userFromDb == null)
            {
                context.Succeed(requirement);
            }
            else if (userFromDb.IsActive) // user exists in db, & we confirmed their email
            {
                context.Succeed(requirement);
            }
            else
            {
                await _signInManager.SignOutAsync();
                context.Fail();
            }
        }
    }
}
