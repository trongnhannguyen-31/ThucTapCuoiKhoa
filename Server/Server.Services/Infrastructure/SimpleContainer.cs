using Falcon.Web.Core.Infrastructure;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Phoenix.Server.Services.Infrastructure
{
    public class SimpleContainer : IContainer
    {
        public SimpleContainer(Container container)
        {
            Container = container;
        }

        public static Container Container { get; private set; }

        public T Resolve<T>() where T : class
        {
            return Container.GetInstance<T>();
        }
	    public T ResolveWithNewScope<T>() where T : class
	    {
		    using (AsyncScopedLifestyle.BeginScope(Container))
		    {
			    return Container.GetInstance<T>();
		    }
	    }
	}
}