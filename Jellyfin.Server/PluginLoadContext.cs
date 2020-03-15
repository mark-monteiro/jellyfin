using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Emby.Server.Implementations
{
    /// <summary>
    /// An <see cref="AssemblyLoadContext"/> for loading plugin assemblies.
    /// </summary>
    public class PluginLoadContext : AssemblyLoadContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginLoadContext"/> class.
        /// </summary>
        public PluginLoadContext()
            : base(isCollectible: true)
        {
        }

        /// <inheritdoc/>
        protected override Assembly? Load(AssemblyName assemblyName)
        {
            return null;
        }
    }
}
