using System.Text.RegularExpressions;

namespace TPS.Web.Core.Models
{
    public static class Utils
    {
        public static string Sanitise(this string s)
        {
            const string pattern = @"[?&^$#@!()+,:;<>’_*\'\-]";
            return Regex.Replace(s, pattern, string.Empty);
        }

    }
}