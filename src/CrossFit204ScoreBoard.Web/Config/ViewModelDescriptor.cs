using System;
using System.Linq;
using FubuMVC.Core.Registration;
using FubuMVC.Validation;

namespace CrossFit204ScoreBoard.Web.Config
{
    public class ViewModelDescriptor : IFubuContinuationModelDescriptor
    {
        private readonly BehaviorGraph graph;

        public ViewModelDescriptor(BehaviorGraph graph)
        {
            this.graph = graph;
        }

        public Type DescribeModelFor(ValidationFailure context)
        {
            var handlerType = context.Target.HandlerType;

            var getCall = graph
                .Behaviors
                .Where(chain => chain.FirstCall() != null
                                && chain.FirstCall().HandlerType == handlerType
                                && chain.Route.AllowedHttpMethods.Contains("GET"))
                .Select(chain => chain.FirstCall())
                .FirstOrDefault();

            if(getCall == null)
                return null;

            return getCall.InputType();
        }
    }
}