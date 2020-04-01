#pragma warning disable CS1591

using System.Text.RegularExpressions;
using MediaBrowser.Model.Entities;
using MediaType = Emby.Naming.Common.MediaType;

namespace Emby.Naming.Video
{
    /// <summary>
    /// A rule used to match a file path with an <see cref="MediaBrowser.Model.Entities.ExtraType"/>.
    /// </summary>
    public class ExtraRule
    {
        private Regex _regex;

        /// <summary>
        /// Gets or sets the token to use for matching against the file path.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the type of the extra to return when matched.
        /// </summary>
        public ExtraType ExtraType { get; set; }

        /// <summary>
        /// Gets or sets the type of the rule.
        /// </summary>
        public ExtraRuleType RuleType { get; set; }

        /// <summary>
        /// Gets or sets the type of the media to return when matched.
        /// </summary>
        public MediaType MediaType { get; set; }

        /// <summary>
        /// Gets a regex constructed using the rule's <see cref="Token"/> string.
        /// </summary>
        public Regex Regex
        {
            get => _regex ?? (_regex = new Regex(Token, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant));
        }
    }
}
