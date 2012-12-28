using System;
using System.Collections.Generic;
using System.Reflection;
using Bottles.Diagnostics;
using Bottles.PackageLoaders.Assemblies;

namespace Bottles
{
    /// <summary>
    /// Configuration DSL layer
    /// </summary>
    public class PackageFacility : IPackageFacility, IPackagingRuntimeGraphConfigurer
    {
        private readonly IList<Action<BottlingRuntimeGraph>> _configurableActions = new List<Action<BottlingRuntimeGraph>>();


        public void Assembly(Assembly assembly)
        {
            addConfigurableAction(g => g.AddLoader(new AssemblyPackageLoader(assembly)));
        }

        public void Bootstrapper(IBootstrapper bootstrapper)
        {
           addConfigurableAction(g => g.AddBootstrapper(bootstrapper));
        }

        public void Loader(IBottleLoader loader)
        {
           addConfigurableAction(g => g.AddLoader(loader));
        }

        public void Facility(IPackageFacility facility)
        {
            addConfigurableAction(graph =>
            {
                graph.AddFacility(facility);
            });
        }

        public void Activator(IActivator activator)
        {
           addConfigurableAction(g => g.AddActivator(activator));
        }

        public void Bootstrap(Func<IPackageLog, IEnumerable<IActivator>> lambda)
        {
            var lambdaBootstrapper = new LambdaBootstrapper(lambda);
            lambdaBootstrapper.Provenance = ProvenanceHelper.GetProvenanceFromStack();

            Bootstrapper(lambdaBootstrapper);
        }
        
        public void Configure(BottlingRuntimeGraph graph)
        {
            _configurableActions.Each(cfgAction => cfgAction(graph));
        }

        private void addConfigurableAction(Action<BottlingRuntimeGraph> action)
        {
            _configurableActions.Add(action);
        }
    }
}