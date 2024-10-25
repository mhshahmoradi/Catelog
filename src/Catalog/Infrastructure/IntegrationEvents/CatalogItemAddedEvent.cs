namespace Catalog.Infrastructure.IntegrationEvents;

public sealed record CatalogItemAddedEvent(
    string Name,
    string Description,
    string CatalogCategory,
    string CatalogBrand,
    string Slug,
    string HintUrl
);