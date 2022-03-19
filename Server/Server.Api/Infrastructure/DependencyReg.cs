using SimpleInjector;

namespace Phoenix.Server.Api.Infrastructure
{
    public static class DependencyReg
    {
        public static void Register(Container container)
        {
            //container.Register<ISmsService, SmsService>(Lifestyle.Scoped);
        }
    }
}