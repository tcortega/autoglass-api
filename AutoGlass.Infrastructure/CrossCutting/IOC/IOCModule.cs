using Autofac;

namespace AutoGlass.Infrastructure.CrossCutting.IOC
{
    public class IOCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            IOCConfiguration.Load(builder);
        }
    }
}