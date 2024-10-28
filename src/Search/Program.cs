using Elastic.Clients.Elasticsearch;
using Elastic.Transport;using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Search;
using Search.Infrastructure.Extenssions;
using Search.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddElasticSearch();

builder.AddBroker();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/search", SearchItem);

app.Run();

static async Task<Results<Ok<IReadOnlyCollection<CatalogItemIndex>>, NotFound>> SearchItem(string term, ElasticsearchClient elasticsearchClient)
{
    var response = await elasticsearchClient.SearchAsync<CatalogItemIndex>(s => 
        s.Index(CatalogItemIndex.IndexName)
            .From(0)
            .Size(10)
            .Query(q => q
                .Fuzzy(t => t.Field(x => x.Description).Value(term)))
    );
    
    if(response.IsValidResponse)
        return TypedResults.Ok(response.Documents);
    
    return TypedResults.NotFound();
}

