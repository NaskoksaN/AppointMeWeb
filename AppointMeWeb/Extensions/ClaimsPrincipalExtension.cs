using System.Security.Claims;

namespace AppointMeWeb.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string Id (this ClaimsPrincipal user)
                    => user.FindFirstValue(ClaimTypes.NameIdentifier);
        
    }
}
