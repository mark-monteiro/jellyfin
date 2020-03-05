#pragma warning disable CS1591
#pragma warning disable SA1600

using System.Globalization;

namespace Jellyfin.Dlna.Common
{
    public class DeviceIcon
    {
        public string Url { get; set; }

        public string MimeType { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Depth { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0}x{1}",
                Height,
                Width);
        }
    }
}
