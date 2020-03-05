#pragma warning disable CS1591
#pragma warning disable SA1600

using System;

namespace Jellyfin.Dlna.PlayTo
{
    public class PlaybackProgressEventArgs : EventArgs
    {
        public uBaseObject MediaInfo { get; set; }
    }
}
