#pragma warning disable CS1591

using System;
using System.IO;
using System.Linq;
using Emby.Naming.Audio;
using Emby.Naming.Common;

namespace Emby.Naming.Video
{
    public class ExtraResolver
    {
        private readonly NamingOptions _options;

        public ExtraResolver(NamingOptions options)
        {
            _options = options;
        }

        public ExtraResult GetExtraInfo(string path)
        {
            return _options.VideoExtraRules
                .Select(i => GetExtraInfo(path, i))
                .FirstOrDefault(i => i.ExtraType != null) ?? ExtraResult.NoMatchResult;
        }

        private ExtraResult GetExtraInfo(string path, ExtraRule rule)
        {
            bool isValidType = rule.MediaType == MediaType.Audio || rule.MediaType == MediaType.Video;
            if (!isValidType)
            {
                return ExtraResult.NoMatchResult;
            }

            if (rule.MediaType == MediaType.Audio && !AudioFileParser.IsAudioFile(path, _options))
            {
                return ExtraResult.NoMatchResult;
            }

            if (rule.MediaType == MediaType.Video && !VideoResolver.IsVideoFile(path, _options))
            {
                return ExtraResult.NoMatchResult;
            }

            // Get the string to match against the rule's regex
            string matchString = rule.RuleType switch
            {
                ExtraRuleType.FileNameRegex => Path.GetFileName(path),
                ExtraRuleType.DirectoryNameRegex => Path.GetFileName(Path.GetDirectoryName(path)),
                _ => throw new InvalidOperationException("Invalid RuleType " + rule.RuleType)
            };

            // Perform the regex match and return based on the result
            if (rule.Regex.IsMatch(matchString))
            {
                return new ExtraResult
                {
                    ExtraType = rule.ExtraType,
                    Rule = rule,
                };
            }
            else
            {
                return ExtraResult.NoMatchResult;
            }
        }
    }
}
