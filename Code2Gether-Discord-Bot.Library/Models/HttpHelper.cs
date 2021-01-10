using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public static class HttpHelper
    {
        /// <summary>
        ///     https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        /// </summary>
        /// <returns>True if <see cref="_userGitHubEmail"/> is valid. Otherwise returns false.</returns>
        public static bool IsValidEmail(string userGitHubEmail)
        {
            if (string.IsNullOrWhiteSpace(userGitHubEmail))
                return false;

            try
            {
                // Normalize the domain
                userGitHubEmail = Regex.Replace(userGitHubEmail, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(userGitHubEmail,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
