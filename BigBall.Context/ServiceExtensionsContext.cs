using BigBall.Common.Entity.DBInterfaces;
using BigBall.Context.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BigBall.Context
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensionsContext
    {
        /// <summary>
        /// Регистрирует все что связано с контекстом
        /// </summary>
        /// <param name="service"></param>
        public static void RegistrationContext(this IServiceCollection service)
        {
            service.TryAddScoped<IBigBallContext>(provider => provider.GetRequiredService<BigBallContext>());
            service.TryAddScoped<IDbRead>(provider => provider.GetRequiredService<BigBallContext>());
            service.TryAddScoped<IDbWriter>(provider => provider.GetRequiredService<BigBallContext>());
            service.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<BigBallContext>());
        }
    }
}
