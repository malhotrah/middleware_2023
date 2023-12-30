namespace NotificationApp1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumerService _consumerService;

        public Worker(ILogger<Worker> logger, IConsumerService consumerService)
        {
            _logger = logger;
            _consumerService = consumerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumerService.ReadMessgaes();
        }
    }
}