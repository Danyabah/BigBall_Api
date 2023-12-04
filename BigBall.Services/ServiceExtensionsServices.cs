using BigBall.General;
using BigBall.Services.Anchor;
using Microsoft.Extensions.DependencyInjection;

namespace BigBall.Services
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensionsServices
    {
        /// <summary>
        /// Регистрирует все что связано с сервисами
        /// </summary>
        /// <param name="service"></param>
        public static void RegistrationContext(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IServiceAnchor>(ServiceLifetime.Scoped);
        }
    }
}
