using NotificationApp1;
using NotificationApp1.Configuration;
using System.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IConsumerService, ConsumerService>();
        services.Configure<RabbitMqConfiguration>(a => context.Configuration.GetSection(nameof(RabbitMqConfiguration)).Bind(a));
        services.AddSingleton<IRabbitMqService, RabbitMqService>();
    })
    .Build();

await host.RunAsync();
