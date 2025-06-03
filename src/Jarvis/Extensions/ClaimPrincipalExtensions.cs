using Jarvis.Const;
using System.Security.Claims;

namespace Jarvis.Extensions
{
    public static class ClaimExtensions
    {
        public static int GetDailyMeta(this ClaimsPrincipal principal)
        {
            var meta = principal.FindFirst(ClaimTypesCustom.DailyMeta);

            if (meta is null)
                return 0;

            if (!int.TryParse(meta.Value, out var dailyMeta))
                return 0;

            return dailyMeta;
        }
        
        public static string? GetProfilePicture(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypesCustom.ProfilePicture)?.Value;
        }

        public static string GetName(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
        }

        public static string GetLastName(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypesCustom.LastName)?.Value ?? string.Empty;
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
        }

        public static string GetToken(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypesCustom.Token)?.Value ?? string.Empty;
        }

        public static int GetId(this ClaimsPrincipal principal)
        {
            var meta = principal.FindFirst(ClaimTypes.NameIdentifier);

            if (meta is null)
                return 0;

            if (!int.TryParse(meta.Value, out var dailyMeta))
                return 0;

            return dailyMeta;
        }
    }
}
