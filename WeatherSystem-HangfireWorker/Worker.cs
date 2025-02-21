namespace WeatherSystem_HangfireWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    // protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    // {
    //     while (!stoppingToken.IsCancellationRequested)
    //     {
    //         _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
    //         await Task.Delay(1000, stoppingToken);
    //     }
    // }

        public override Task StartAsync(CancellationToken cancellationToken)
    {
        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask; // Hangfire handles job execution
    }
}
