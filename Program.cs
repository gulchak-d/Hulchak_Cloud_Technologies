using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddAzureClients(b =>
        {
            b.AddBlobServiceClient(Environment.GetEnvironmentVariable("blobConn")!);
            var endpoint = new Uri(Environment.GetEnvironmentVariable("textAnalyticsEndpoint")!);
            var credential = new AzureKeyCredential(Environment.GetEnvironmentVariable("textAnalyticskey")!);
            b.AddTextAnalyticsClient(endpoint, credential);
        });
    })
    .Build();

host.Run();
