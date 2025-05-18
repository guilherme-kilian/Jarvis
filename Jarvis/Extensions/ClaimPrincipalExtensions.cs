using Jarvis.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
        }
    }
}
