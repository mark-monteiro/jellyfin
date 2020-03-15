using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using Emby.Server.Implementations;
using MediaBrowser.Common.Net;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Controller.Drawing;
using MediaBrowser.Model.IO;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Server
{
    /// <summary>
    /// Implementation of the abstract <see cref="ApplicationHost" /> class.
    /// </summary>
    public class CoreAppHost : ApplicationHost
    {
        private readonly Dictionary<Assembly, AssemblyLoadContext> _pluginLoadContexts;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreAppHost" /> class.
        /// </summary>
        /// <param name="applicationPaths">The <see cref="ServerApplicationPaths" /> to be used by the <see cref="CoreAppHost" />.</param>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory" /> to be used by the <see cref="CoreAppHost" />.</param>
        /// <param name="options">The <see cref="StartupOptions" /> to be used by the <see cref="CoreAppHost" />.</param>
        /// <param name="fileSystem">The <see cref="IFileSystem" /> to be used by the <see cref="CoreAppHost" />.</param>
        /// <param name="imageEncoder">The <see cref="IImageEncoder" /> to be used by the <see cref="CoreAppHost" />.</param>
        /// <param name="networkManager">The <see cref="INetworkManager" /> to be used by the <see cref="CoreAppHost" />.</param>
        public CoreAppHost(
            ServerApplicationPaths applicationPaths,
            ILoggerFactory loggerFactory,
            StartupOptions options,
            IFileSystem fileSystem,
            IImageEncoder imageEncoder,
            INetworkManager networkManager)
            : base(
                applicationPaths,
                loggerFactory,
                options,
                fileSystem,
                imageEncoder,
                networkManager)
        {
            _pluginLoadContexts = new Dictionary<Assembly, AssemblyLoadContext>();
        }

        /// <inheritdoc />
        public override bool CanSelfRestart => StartupOptions.RestartPath != null;

        /// <inheritdoc />
        protected override void RestartInternal() => Program.Restart();

        /// <inheritdoc/>
        protected override Assembly LoadPluginAssembly(string assemblyPath)
        {
            var loadContext = new PluginLoadContext();
            var assembly = loadContext.LoadFromAssemblyPath(assemblyPath);
            _pluginLoadContexts.Add(assembly, loadContext);
            return assembly;
        }

        /// <inheritdoc/>
        public override void RemovePlugin(IPlugin plugin)
        {
            base.RemovePlugin(plugin);

            // Get the assembly load context for this plugin, if any, and remove it from the internal dictionary
            var pluginAssembly = plugin.GetType().Assembly;
            _pluginLoadContexts.Remove(pluginAssembly, out var pluginLoadContext);

            // Unload the assembly
            // See: https://docs.microsoft.com/en-us/dotnet/standard/assembly/unloadability#use-a-custom-collectible-assemblyloadcontext
            if (pluginLoadContext != null)
            {
                // Unload the plugin assembly
                var loadContextRef = new WeakReference(pluginLoadContext, trackResurrection: true);
                pluginLoadContext.Unload();

                // Wait for unload completion
                for (int i = 0; loadContextRef.IsAlive && (i < 10); i++)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

        /// <inheritdoc />
        protected override IEnumerable<Assembly> GetAssembliesWithPartsInternal()
        {
            yield return typeof(CoreAppHost).Assembly;
        }

        /// <inheritdoc />
        protected override void ShutdownInternal() => Program.Shutdown();
    }
}
