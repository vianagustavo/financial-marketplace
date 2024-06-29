using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FinancialMarketplace.Application.Services.Workers;

public class WorkersConfigurationBuilder(
    ILogger<WorkersConfigurationBuilder> logger,
    INotifyExpiringProducts notifyExpiringProducts) : IHostedService, IDisposable
{
    private readonly ILogger<WorkersConfigurationBuilder> _logger = logger;
    private readonly INotifyExpiringProducts _notifyExpiringProducts = notifyExpiringProducts;
    private Timer? _timer;
    private bool _disposed;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Workers are configured and are starting.");

        TimeSpan timeUntilMidnight = DateTime.Today.AddDays(1) - DateTime.Now;
        _timer = new Timer(DoWork, null, timeUntilMidnight, TimeSpan.FromDays(1));

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        _notifyExpiringProducts.NotifyExpiringProducts().Wait();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Workers Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _timer?.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
