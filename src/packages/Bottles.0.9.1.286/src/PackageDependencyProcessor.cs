using System;
using System.Collections.Generic;
using Bottles.Diagnostics;
using FubuCore.DependencyAnalysis;
using System.Linq;
using FubuCore;

namespace Bottles
{
    public class PackageDependencyProcessor
    {
        private readonly IEnumerable<IPackageInfo> _packages;
        private readonly DependencyGraph<IPackageInfo> _graph;

        public PackageDependencyProcessor(IEnumerable<IPackageInfo> packages)
        {
            _packages = packages;

            guardAgainstMalformedPackages();

            _graph = new DependencyGraph<IPackageInfo>(pak => pak.Name, pak => pak.Dependencies.Select(x => x.Name));

            _packages.OrderBy(p => p.Name).Each(p => _graph.RegisterItem(p));
        }

        public void LogMissingPackageDependencies(IPackagingDiagnostics diagnostics)
        {
            var missingDependencies = _graph.MissingDependencies();
            missingDependencies.Each(name =>
            {
                var dependentPackages = _packages.Where(pak => pak.Dependencies.Any(dep => dep.IsMandatory && dep.Name == name));
                dependentPackages.Each(pak => diagnostics.LogFor(pak).LogMissingDependency(name));
            });
        }

        public IEnumerable<IPackageInfo> OrderedPackages()
        {
            return _graph.Ordered();
        }

        private void guardAgainstMalformedPackages()
        {
            var missing = _packages.Where(p => p.Name.IsEmpty());
            if (missing.Any())
            {
                var moduleTypes = missing.Select(x => x.Role).Join(", ");
                throw new ArgumentException("Packages must have a name ({0}, {1})".ToFormat(missing.Count(), moduleTypes));
            }
        }
    }

    public static class PackageLogDependencyExtensions
    {
        public static void LogMissingDependency(this IPackageLog log, string dependencyName)
        {
            log.MarkFailure("Missing required Bottle/Package dependency named '{0}'".ToFormat(dependencyName));
        }
    }
}