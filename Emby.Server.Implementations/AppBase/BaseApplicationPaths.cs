using System;
using System.IO;
using MediaBrowser.Common.Configuration;

namespace Emby.Server.Implementations.AppBase
{
    /// <summary>
    /// Provides a base class to hold common application paths used by both the Ui and Server.
    /// This can be subclassed to add application-specific paths.
    /// </summary>
    public abstract class BaseApplicationPaths : IApplicationPaths
    {
        private string _dataPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApplicationPaths"/> class.
        /// </summary>
        protected BaseApplicationPaths(
            string programDataPath,
            string logDirectoryPath,
            string configurationDirectoryPath,
            string cacheDirectoryPath,
            string webDirectoryPath)
        {
            ProgramDataPath = programDataPath;
            LogDirectoryPath = logDirectoryPath;
            ConfigurationDirectoryPath = configurationDirectoryPath;
            CachePath = cacheDirectoryPath;
            WebPath = webDirectoryPath;

            DataPath = Path.Combine(ProgramDataPath, "data");
        }

        /// <inheritdoc/>
        public string ProgramDataPath { get; }

        /// <summary>
        /// Gets the path to the web UI resources folder.
        /// </summary>
        /// <value>The web UI resources path.</value>
        public string WebPath { get; }

        /// <inheritdoc/>
        public string ProgramSystemPath { get; } = AppContext.BaseDirectory;

        /// <inheritdoc/>
        public string DataPath
        {
            get => _dataPath;
            private set => _dataPath = Directory.CreateDirectory(value).FullName;
        }

        /// <inheritdoc />
        public string VirtualDataPath { get; } = "%AppDataPath%";

        /// <inheritdoc/>
        public string ImageCachePath => Path.Combine(CachePath, "images");

        /// <inheritdoc/>
        public string PluginsPath => Path.Combine(ProgramDataPath, "plugins");

        /// <inheritdoc/>
        public string PluginConfigurationsPath => Path.Combine(PluginsPath, "configurations");

        /// <inheritdoc/>
        public string LogDirectoryPath { get; }

        /// <inheritdoc/>
        public string ConfigurationDirectoryPath { get; }

        /// <inheritdoc/>
        public string SystemConfigurationFilePath => Path.Combine(ConfigurationDirectoryPath, "system.xml");

        /// <inheritdoc/>
        public string CachePath { get; set; }

        /// <inheritdoc/>
        public string TempDirectory => Path.Combine(CachePath, "temp");
    }
}
