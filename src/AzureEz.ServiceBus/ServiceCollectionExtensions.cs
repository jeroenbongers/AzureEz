using AzureEz.ServiceBus.Infrastructure;
using AzureEz.ServiceBus.Infrastructure.Interfaces;
using AzureEz.ServiceBus.Services;
using AzureEz.ServiceBus.Services.Interfaces;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureEz.ServiceBus;


public static class ServiceCollectionExtensions
{
    /// <summary>
    /// <para>Registers the AzureEz.ServiceBus helper class <see cref="IServiceBusFactory"/>.</para>
    /// <para>Additionally this method also registers the base <see cref="Azure.Messaging.ServiceBus.ServiceBusClient"/> required for connecting to Azure ServiceBus.</para>
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance to which the services should be added.</param>
    /// <param name="configuration">Instance of <see cref="IConfiguration"/> containing ServiceBus connection string.</param>
    public static IServiceCollection AddAzureEzServiceBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAzureClients(builder => builder.AddServiceBusClient(configuration));
        services.AddSingleton<IServiceBusFactory, ServiceBusFactory>();
        services.AddScoped<IServiceBusService, ServiceBusService>();
        
        return services;
    }
    
    /// <summary>
    /// <para>Registers the AzureEz.ServiceBus helper class <see cref="IServiceBusFactory"/>.</para> 
    /// <para>Additionally this method also registers the base <see cref="Azure.Messaging.ServiceBus.ServiceBusClient"/> required for connecting to Azure ServiceBus.</para>
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance to which the services should be added.</param>
    /// <param name="connectionString">The connection string required to connect to Azure ServiceBus</param>
    public static IServiceCollection AddAzureEzServiceBus(this IServiceCollection services, string connectionString)
    {
        services.AddAzureClients(builder => builder.AddServiceBusClient(connectionString));
        services.AddSingleton<IServiceBusFactory, ServiceBusFactory>();
        
        return services;
    }
}