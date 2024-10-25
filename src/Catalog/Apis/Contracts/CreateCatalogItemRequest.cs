
namespace Catalog.Apis.Contracts;

public sealed record CreateCatalogItemRequest(
    string Name,
    string Description,
    int CategoryId,
    int BrandId,
    int MaxStockThreshold);

public sealed class CreateCatalogItemRequestValidator : AbstractValidator<CreateCatalogItemRequest>
{
    public CreateCatalogItemRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull()
            .MaximumLength(5000);

        RuleFor(x => x.CategoryId)
            .NotNull();

        RuleFor(x => x.BrandId)
            .NotNull();

        RuleFor(x => x.MaxStockThreshold)
             .GreaterThan(0);
    }
}