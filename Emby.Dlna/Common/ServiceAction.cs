#pragma warning disable CS1591
#pragma warning disable SA1600

using System.Collections.Generic;

namespace Jellyfin.Dlna.Common
{
    public class ServiceAction
    {
        public ServiceAction()
        {
            ArgumentList = new List<Argument>();
        }

        public string Name { get; set; }

        public List<Argument> ArgumentList { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }
    }
}
