using System.Reflection;
using MassTransit;

namespace Search.Infrastructure.Extenssions;

public static class BrokerConfiguration
{
    public static IHostApplicationBuilder AddBroker(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(configure =>
        {
            configure.AddConsumers(Assembly.GetExecutingAssembly());
            
            configure.UsingRabbitMq((context, cfg) =>
            {
                var host = builder.Configuration.GetConnectionString("messaging");
                cfg.Host(host);

                cfg.ConfigureEndpoints(context);
            });
        });
        
        return builder;
    }
}