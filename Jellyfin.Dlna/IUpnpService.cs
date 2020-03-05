#pragma warning disable CS1591
#pragma warning disable SA1600

using System.Threading.Tasks;

namespace Jellyfin.Dlna
{
    public interface IUpnpService
    {
        /// <summary>
        /// Gets the content directory XML.
        /// </summary>
        /// <returns>System.String.</returns>
        string GetServiceXml();

        /// <summary>
        /// Processes the control request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ControlResponse.</returns>
        Task<ControlResponse> ProcessControlRequestAsync(ControlRequest request);
    }
}
