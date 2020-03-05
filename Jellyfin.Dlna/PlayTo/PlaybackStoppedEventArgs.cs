#pragma warning disable CS1591
#pragma warning disable SA1600

using System;

namespace Jellyfin.Dlna.PlayTo
{
    public class PlaybackStoppedEventArgs : EventArgs
    {
        public uBaseObject MediaInfo { get; set; }
    }

    public class MediaChangedEventArgs : EventArgs
    {
        public uBaseObject OldMediaInfo { get; set; }
        public uBaseObject NewMediaInfo { get; set; }
    }
}
