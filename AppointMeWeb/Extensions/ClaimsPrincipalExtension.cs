using System.Security.Claims;

using static AppointMeWeb.Infrastrucure.Constants.DataConstants;

namespace AppointMeWeb.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRole);
        }

        public static bool IsUser(this ClaimsPrincipal user)
        {
            return user.IsInRole(WebUserRole);
        }

        public static bool IsBusinessProvider(this ClaimsPrincipal user)
        {
            return user.IsInRole(BusinessRole);
        }


    }
}
