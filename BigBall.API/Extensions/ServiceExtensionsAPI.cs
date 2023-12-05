using BigBall.Common.Entity.DBInterfaces;
using BigBall.Common.Entity;
using BigBall.Services.Mapper;
using Microsoft.OpenApi.Models;
using BigBall.Context;
using BigBall.Repositories;
using BigBall.Services;
using BigBall.API.Mappers;
using Newtonsoft.Json.Converters;

namespace BigBall.API.Extensions
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensionsAPI
    {
        /// <summary>
        /// Регистрирует все сервисы, репозитории и все что нужно для контекста
        /// </summary>
        public static void RegistrationSRC(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IDbWriterContext, DbWriterContext>();
            services.RegistrationContext();
            services.RegistrationRepository();
            services.RegistrationServices();
            services.AddAutoMapper(typeof(APIMapper), typeof(ServiceMapper));
        }

        /// <summary>
        /// Включает фильтры и ставит шрифт на перечесления
        /// </summary>
        /// <param name="services"></param>
        public static void RegistrationControllers(this IServiceCollection services)
        {
            services.AddControllers(x =>
            {
                x.Filters.Add<BigBallExceptionFilter>();
            })
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.Converters.Add(new StringEnumConverter
                    {
                        CamelCaseText = false
                    });
                });
        }

        /// <summary>
        /// Настройки свагера
        /// </summary>
        public static void RegistrationSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Institution", new OpenApiInfo { Title = "Клубы", Version = "v1" });
                c.SwaggerDoc("Payment", new OpenApiInfo { Title = "Способы оплаты", Version = "v1" });
                c.SwaggerDoc("Person", new OpenApiInfo { Title = "Клиенты", Version = "v1" });
                c.SwaggerDoc("Promotion", new OpenApiInfo { Title = "Скидки", Version = "v1" });
                c.SwaggerDoc("Reservation", new OpenApiInfo { Title = "Заказы", Version = "v1" });
                c.SwaggerDoc("Track", new OpenApiInfo { Title = "Дорожки", Version = "v1" });

                var filePath = Path.Combine(AppContext.BaseDirectory, "BigBall.API.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        /// <summary>
        /// Настройки свагера
        /// </summary>
        public static void CustomizeSwaggerUI(this WebApplication web)
        {
            web.UseSwagger();
            web.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("Institution/swagger.json", "Клубы");
                x.SwaggerEndpoint("Payment/swagger.json", "Способы оплаты");
                x.SwaggerEndpoint("Person/swagger.json", "Клиенты");
                x.SwaggerEndpoint("Promotion/swagger.json", "Скидки");
                x.SwaggerEndpoint("Reservation/swagger.json", "Заказы");
                x.SwaggerEndpoint("Track/swagger.json", "Дорожки");
            });
        }
    }
}
