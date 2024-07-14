using Autofac;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence
{
    public class PersistenceRegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(this.GetType().Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();


            builder.RegisterType<DataContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();
        }
    }
}
