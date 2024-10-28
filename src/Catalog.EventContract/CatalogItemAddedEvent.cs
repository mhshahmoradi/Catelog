namespace Catalog.EventContract;

public sealed record CatalogItemAddedEvent(
    string Name,
    string Description,
    string CatalogCategory,
    string CatalogBrand,
    string Slug,
    string HintUrl
);