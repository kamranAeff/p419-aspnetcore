using Autofac;
using Domain.Entities.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

            builder.Register(ctx =>
            {
                var services = new ServiceCollection();
                Console.WriteLine("ok");
                services.AddIdentity<OganiUser, OganiRole>()
                        .AddEntityFrameworkStores<DataContext>()
                        .AddDefaultTokenProviders();

                // Add any other identity configurations here

                return services;
            })
            .As<IServiceCollection>().InstancePerLifetimeScope();
        }
    }
}
