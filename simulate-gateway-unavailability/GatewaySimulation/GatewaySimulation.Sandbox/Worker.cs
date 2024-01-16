namespace GatewaySimulation.Sandbox;

public class Worker : BackgroundService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        this._httpClient = new HttpClient();
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            var tenantId = "72f988bf-86f1-41af-91ab-2d7cd011db47";
            var subscriptionId = "b86ca785-448d-441e-8ed9-b75f95ff31a5";
            var resourceGroupName = "building-resilient-api-platform";
            var serviceName = "building-resilient-api-platform-in-azure";
            
            var authRequest = new HttpRequestMessage(HttpMethod.Post, $"https://login.microsoftonline.com/{tenantId}/oauth2/token");
            var response = await _httpClient.SendAsync(authRequest, stoppingToken);

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.ApiManagement/service/{serviceName}?api-version=2022-08-01");
            var response = await _httpClient.SendAsync(request, stoppingToken);
            var responseBody = await response.Content.ReadAsStringAsync(stoppingToken);
            _logger.LogInformation("Response '{StatusCode}': {Payload}", response.StatusCode, responseBody);
            
            await Task.Delay(1000, stoppingToken);
        }
    }
}