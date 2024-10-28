using Catalog.EventContract;
using Elastic.Clients.Elasticsearch;
using MassTransit;
using Search.Models;

namespace Search.Infrastructure.Consumers;

public class CatalogItemAddedEventConsumer(ElasticsearchClient elasticsearchClient) 
    : IConsumer<CatalogItemAddedEvent>
{
    private readonly ElasticsearchClient _elasticsearchClient = elasticsearchClient;
    
    public async Task Consume(ConsumeContext<CatalogItemAddedEvent> context)
    {
        var message = context.Message;

        var itemIndex = new CatalogItemIndex
        {
            Name = message.Name,
            Description = message.Description,
            Slug = message.Slug,
            CatalogBrand = message.CatalogBrand,
            CatalogCategory = message.CatalogCategory,
            HintUrl = message.HintUrl
        };
        
        var indexExist = await _elasticsearchClient.Indices.ExistsAsync(CatalogItemIndex.IndexName);

        if (!indexExist.Exists)
        {
            await _elasticsearchClient.Indices
                .CreateAsync(index: CatalogItemIndex.IndexName);
        }
        
        var response = await _elasticsearchClient
            .IndexAsync(itemIndex, index: CatalogItemIndex.IndexName, context.CancellationToken);
            
        if (response.IsValidResponse)
            Console.WriteLine($"Index {itemIndex.Name} has been successfully added to Elasticsearch");

    }
}