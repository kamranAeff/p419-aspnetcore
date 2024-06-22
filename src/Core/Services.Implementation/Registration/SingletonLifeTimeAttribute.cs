using Microsoft.Extensions.DependencyInjection;

namespace Services.Implementation.Registration
{
    [AttributeUsage(AttributeTargets.Class)]
    class SingletonLifeTimeAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    class LifeTimeAttribute : Attribute
    {
        public ServiceLifetime LifetimeMode { get; set; }
    }
}
