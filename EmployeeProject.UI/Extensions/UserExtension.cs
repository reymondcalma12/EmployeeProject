using System.Security.Claims;

namespace EmployeeProject.UI.Extensions
{
    public static class UserExtension
    {
        public static string GetUsername(this ClaimsPrincipal principal)
        {
            var usernameClaim = principal.FindFirst(ClaimTypes.Name);
            return usernameClaim?.Value ?? string.Empty;
        }

        public static string GetUserId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim?.Value ?? string.Empty;
        }

        public static string GetEmailAddress(this ClaimsPrincipal principal)
        {
            var emailClaim = principal.FindFirst(ClaimTypes.Email);
            return emailClaim?.Value ?? string.Empty;
        }



    }
}
