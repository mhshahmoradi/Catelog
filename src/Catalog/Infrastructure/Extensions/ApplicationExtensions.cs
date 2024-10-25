using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Catalog.Infrastructure.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<CatalogDbContext>(configure =>
        {
            configure.UseSqlServer(builder.Configuration.GetConnectionString(CatalogDbContext.DefaultConnectionStringName));
        });

        builder.Services.AddMassTransit(configure =>
        {
            configure.UsingRabbitMq((context, cfg) =>
            {
                var host = builder.Configuration.GetConnectionString("messaging");
                cfg.Host(host);

                cfg.ConfigureEndpoints(context);
            });
        });

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Services.AddOptions<CatalogOptions>()
                        .BindConfiguration(nameof(CatalogOptions));
    }
}
