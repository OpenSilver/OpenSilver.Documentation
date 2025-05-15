var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder
    .AddProject<Projects.OpenSilverAndAspire_ApiService>("apiservice")
    .WithExternalHttpEndpoints();

builder
    .AddProject<Projects.OpenSilverAndAspire_WasmHost>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();