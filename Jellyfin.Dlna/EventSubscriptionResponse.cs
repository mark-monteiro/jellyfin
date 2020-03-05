#pragma warning disable CS1591
#pragma warning disable SA1600

using System.Collections.Generic;

namespace Jellyfin.Dlna
{
    public class EventSubscriptionResponse
    {
        public EventSubscriptionResponse()
        {
            Headers = new Dictionary<string, string>();
        }

        public string Content { get; set; }

        public string ContentType { get; set; }

        public Dictionary<string, string> Headers { get; set; }
    }
}
