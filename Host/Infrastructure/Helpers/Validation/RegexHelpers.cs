using System.Text.RegularExpressions;

namespace Host.Infrastructure.Helpers.Validation
{
    public static class RegexHelpers
    {
        public static bool IsMatch(this Regex[] regexes, string value) => regexes.All(x => x.IsMatch(value));
    }
}
