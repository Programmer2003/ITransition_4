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

        public ActiveUserRequirementHandler(UserManager<UserProfile> userManager, SignInManager<UserProfile> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ActiveUserRequirement requirement)
        {
            var userFromDb = await _userManager.GetUserAsync(context.User);


            if (userFromDb == null)
            {
                context.Succeed(requirement);
            }
            else if (userFromDb.IsActive)
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
