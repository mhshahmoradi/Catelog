using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var catalog = builder.AddProject<Catalog>("Catalog");

builder.Build().Run();