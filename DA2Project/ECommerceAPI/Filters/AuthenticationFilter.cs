using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Filters
{
    public class AuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string headerToken = context.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrWhiteSpace(headerToken))
            {
                Authenticate(context);
            }
            else
            {
                bool isValidToken = Guid.TryParse(headerToken, out Guid tokenGuid);

                if (!isValidToken)
                {
                    SetUnauthorizedResult(context, "Token format is invalid");
                }
                else
                {
                    VerifyToken(tokenGuid, context);
                }
            }
        }

        private void Authenticate(AuthorizationFilterContext context)
        {
            var authService = GetAuthService(context);

            try
            {
                var currentUser = authService.GetCurrentUser();

                if (currentUser is null)
                {
                    SetUnauthorizedResult(context, "Token is required");
                }
                else
                {
                    context.HttpContext.Items.Add("user", currentUser);
                }
            }
            catch (Exception)
            {
                SetUnauthorizedResult(context, "Token is required");
            }
        }

        private void SetUnauthorizedResult(AuthorizationFilterContext context, string content)
        {
            context.Result = new ContentResult()
            {
                Content = content,
                StatusCode = 401
            };
        }

        private void VerifyToken(Guid token, AuthorizationFilterContext context)
        {
            var userService = GetUserService(context);

            if (!userService.IsValidToken(token))
            {
                SetUnauthorizedResult(context, "Invalid Token");
            }
            else
            {
                User user = userService.GetUserByToken(token);
                context.HttpContext.Items.Add("user", user);
            }
        }

        private IUserService GetUserService(AuthorizationFilterContext context)
        {
            return context.HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService;
        }

        private IAuthService GetAuthService(AuthorizationFilterContext context)
        {
            return context.HttpContext.RequestServices.GetService(typeof(IAuthService)) as IAuthService;
        }
    }
}
