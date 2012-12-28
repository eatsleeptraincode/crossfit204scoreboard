using System;
using System.Collections.Generic;
using Bottles.Diagnostics;

namespace Bottles
{
    [Obsolete]
    public interface IPackageLoader : IBottleLoader
    {
    }

    /// <summary>
    /// Finds bottles
    /// </summary>
    public interface IBottleLoader
    {
        /// <summary>
        /// The load method should find all packages and return them in the enum
        /// </summary>
        /// <param name="log">log for logging</param>
        /// <returns>found package infos</returns>
        IEnumerable<IPackageInfo> Load(IPackageLog log);
    }
}