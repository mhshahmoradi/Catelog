namespace Catalog;

public sealed class CatalogOptions
{
    public MediaOptions MediaOptions { get; set; } = null!;
}
  
public sealed class MediaOptions
{
    public required string AccessKey { get; set; }
    public required string SecretKey { get; set; }
    public required string BucketName { get; set; }
    public required string Endpoint { get; set; }
}


