namespace Search;

public sealed class AppSettings
{
    public required ElasticSearchConfigurations ElasticSearchConfigurations { get; set; }
}

public sealed class ElasticSearchConfigurations
{
    public required string Host { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string FingerPrint { get; set; }
}