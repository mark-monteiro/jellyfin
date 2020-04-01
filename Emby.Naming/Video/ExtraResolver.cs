#pragma warning disable CS1591

using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
                .FirstOrDefault(i => i.ExtraType != null) ?? new ExtraResult();
        }

        private ExtraResult GetExtraInfo(string path, ExtraRule rule)
        {
            var result = new ExtraResult();

            if (rule.MediaType == MediaType.Audio)
            {
                if (!AudioFileParser.IsAudioFile(path, _options))
                {
                    return result;
                }
            }
            else if (rule.MediaType == MediaType.Video)
            {
                if (!new VideoResolver(_options).IsVideoFile(path))
                {
                    return result;
                }
            }
            else
            {
                return result;
            }

            if (rule.RuleType == ExtraRuleType.FileNameRegex)
            {
                var filename = Path.GetFileName(path);

                var regex = new Regex(rule.Token, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                if (regex.IsMatch(filename))
                {
                    result.ExtraType = rule.ExtraType;
                    result.Rule = rule;
                }
            }
            else if (rule.RuleType == ExtraRuleType.DirectoryNameRegex)
            {
                var directoryName = Path.GetFileName(Path.GetDirectoryName(path));

                var regex = new Regex(rule.Token, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                if (regex.IsMatch(directoryName))
                {
                    result.ExtraType = rule.ExtraType;
                    result.Rule = rule;
                }
            }

            return result;
        }
    }
}
