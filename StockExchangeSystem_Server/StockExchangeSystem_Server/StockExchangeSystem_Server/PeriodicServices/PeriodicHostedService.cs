namespace StockExchangeSystem_Server.PeriodicServices
{
    public class PeriodicHostedService : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromMinutes(20);
        private readonly IServiceScopeFactory _factory;
        private readonly ILogger<PeriodicHostedService> _logger;

        public PeriodicHostedService(IServiceScopeFactory factory, ILogger<PeriodicHostedService> logger)
        {
            _factory = factory;
            _logger = logger;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (
                !stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                    var sampleService = asyncScope.ServiceProvider.GetRequiredService<IRefreshLogic>();
                    await sampleService.Refresh();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while refreshing");
                }
                finally
                {
                    _logger.LogInformation("Refreshing finished");
                }

            }
        }
    }
}
