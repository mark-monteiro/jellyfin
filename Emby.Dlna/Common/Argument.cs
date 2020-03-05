#pragma warning disable CS1591
#pragma warning disable SA1600

namespace Jellyfin.Dlna.Common
{
    public class Argument
    {
        public string Name { get; set; }

        public string Direction { get; set; }

        public string RelatedStateVariable { get; set; }
    }
}
