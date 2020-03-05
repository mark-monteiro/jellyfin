#pragma warning disable CS1591
#pragma warning disable SA1600

using System;

namespace Jellyfin.Dlna.Eventing
{
    public class EventSubscription
    {
        public string Id { get; set; }
        public string CallbackUrl { get; set; }
        public string NotificationType { get; set; }

        public DateTime SubscriptionTime { get; set; }
        public int TimeoutSeconds { get; set; }

        public long TriggerCount { get; set; }

        public bool IsExpired => SubscriptionTime.AddSeconds(TimeoutSeconds) >= DateTime.UtcNow;

        public void IncrementTriggerCount()
        {
            if (TriggerCount == long.MaxValue)
            {
                TriggerCount = 0;
            }

            TriggerCount++;
        }
    }
}
