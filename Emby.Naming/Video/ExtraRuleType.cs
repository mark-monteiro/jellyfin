#pragma warning disable CS1591

namespace Emby.Naming.Video
{
    public enum ExtraRuleType
    {
        /// <summary>
        /// Match <see cref="ExtraRule.Token"/> against the a regex.
        /// </summary>
        FileNameRegex = 0,

        /// <summary>
        /// Match <see cref="ExtraRule.Token"/> against the name of the directory containing the file.
        /// </summary>
        DirectoryNameRegex = 1,
    }
}
