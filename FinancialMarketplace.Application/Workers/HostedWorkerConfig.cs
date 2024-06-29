using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FinancialMarketplace.Application.Services.Workers;

public class WorkersConfigurationBuilder(IServiceProvider serviceProvider) : IHostedService, IDisposable
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private Timer? _timer;
    private bool _disposed;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        TimeSpan timeUntilMidnight = DateTime.Today.AddDays(1) - DateTime.Now;
        _timer = new Timer(DoWork, null, timeUntilMidnight, TimeSpan.FromDays(1));

        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        using var scope = _serviceProvider.CreateScope();
        var notifyExpiringProducts = scope.ServiceProvider.GetRequiredService<INotifyExpiringProducts>();
        await notifyExpiringProducts.NotifyExpiringProducts();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
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
