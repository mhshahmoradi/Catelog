namespace Catalog.EventContract;

public record CatalogItemChangedEvent(
    string Name,
    string Description,
    string CatalogCategory,
    string CatalogBrand,
    string Slug,
    string HintUrl
);