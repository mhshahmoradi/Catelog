using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Options;

namespace Search.Infrastructure.Extenssions;

public static class ElasticSearchConfiguration
{
    public static IServiceCollection AddElasticSearch(this IServiceCollection services)
    {
        services.AddScoped<ElasticsearchClient>(sp =>
        {
            var elasticSettings = sp.GetService<IOptions<AppSettings>>()!.Value.ElasticSearchConfigurations;
    
            var settings = new ElasticsearchClientSettings(new Uri(elasticSettings.Host))
                .CertificateFingerprint(elasticSettings.FingerPrint)
                .Authentication(new BasicAuthentication(elasticSettings.Username, elasticSettings.Password));

            return new ElasticsearchClient(settings);
        });
        
        return services;
    }
}