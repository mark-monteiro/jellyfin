using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;

namespace MediaBrowser.Providers.Plugins.Tmdb.People
{
    public class TmdbPersonExternalId : IExternalId
    {
        /// <inheritdoc />
        public string Name => TmdbUtils.ProviderName;

        /// <inheritdoc />
        public string Key => MetadataProvider.Tmdb.ToString();

        /// <inheritdoc />
        public string UrlFormatString => TmdbUtils.BaseTmdbUrl + "person/{0}";

        /// <inheritdoc />
        public bool Supports(IHasProviderIds item)
        {
            return item is Person;
        }
    }
}
