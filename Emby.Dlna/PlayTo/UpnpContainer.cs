#pragma warning disable CS1591
#pragma warning disable SA1600

using System;
using System.Xml.Linq;
using Jellyfin.Dlna.Ssdp;

namespace Jellyfin.Dlna.PlayTo
{
    public class UpnpContainer : uBaseObject
    {
        public static uBaseObject Create(XElement container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            return new uBaseObject
            {
                Id = container.GetAttributeValue(uPnpNamespaces.Id),
                ParentId = container.GetAttributeValue(uPnpNamespaces.ParentId),
                Title = container.GetValue(uPnpNamespaces.title),
                IconUrl = container.GetValue(uPnpNamespaces.Artwork),
                UpnpClass = container.GetValue(uPnpNamespaces.uClass)
            };
        }
    }
}
