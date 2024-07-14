using Autofac;
using Domain.Entities.Membership;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Services.Implementation.Registration
{
    public class ServiceRegisterModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var types = this.GetType().Assembly.GetTypes()
                .Where(m => m.IsClass)
                .Select(m => new
                {
                    Type = m,
                    IsSingleton = m.IsDefined(typeof(SingletonLifeTimeAttribute))
                });


            builder.RegisterTypes(types.Where(m => m.IsSingleton).Select(m => m.Type).ToArray())
               .AsImplementedInterfaces()
               .SingleInstance();

            builder.RegisterTypes(types.Where(m => m.IsSingleton == false).Select(m => m.Type).ToArray())
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
        }
    }
}
