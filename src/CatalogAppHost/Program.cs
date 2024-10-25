using CatalogAppHost.Extensions;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var brokerUsername = builder.AddParameter("BrokerUsername", true);
var brokerPassword = builder.AddParameter("BrokerPassword", true);

var rabbitmq = builder.AddRabbitMQ("messaging", brokerUsername, brokerPassword)
    .WithDataVolume(isReadOnly: false)
    .WithManagementPlugin(5328);

var catalog = builder.AddProject<Catalog>("Catalog")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);

var search = builder.AddProject<Search>("Search");

builder.Build().Run();