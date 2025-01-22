namespace HealthIndicators.Services;

public class KeepAliveService : IHostedService, IDisposable {
    private Timer _timer;
    private readonly HttpClient _httpClient;
    private readonly string _url;
    private readonly ILogger<KeepAliveService> _logger;
    

    public KeepAliveService(IConfiguration configuration, ILogger<KeepAliveService> logger) {
        _httpClient = new HttpClient();
        _logger = logger;
        _url = configuration.GetSection("urlInstance")?.Get<string>() ?? string.Empty;
        Console.WriteLine($"Keep-alive URL: {_url}");
    }

    public Task StartAsync(CancellationToken cancellationToken) {
        _logger.LogInformation("Keep-alive service started");
        _timer = new Timer(SendKeepAliveRequest, null, TimeSpan.Zero, TimeSpan.FromMinutes(14));
        return Task.CompletedTask;
    }

    private async void SendKeepAliveRequest(object state)
    {
        try
        {
            var response = await _httpClient.GetAsync(_url);
            if (response.IsSuccessStatusCode)
                Console.WriteLine($"Keep-alive successful at {DateTime.Now}");
            else
                Console.WriteLine($"Failed keep-alive at {DateTime.Now}: {response.StatusCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in keep-alive: {ex.Message}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
        _httpClient?.Dispose();
    }
}
