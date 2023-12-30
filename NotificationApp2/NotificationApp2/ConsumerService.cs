using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace NotificationApp2
{
    public interface IConsumerService
    {
        Task ReadMessgaes();
    }

    public class ConsumerService : IConsumerService, IDisposable
    {
        private readonly IModel _model;
        private readonly IConnection _connection;
        public ConsumerService(IRabbitMqService rabbitMqService)
        {
            _connection = rabbitMqService.CreateChannel();
            _model = _connection.CreateModel();
        }
        const string _queueName = "notification2_service_queue";
        public async Task ReadMessgaes()
        {
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var text = System.Text.Encoding.UTF8.GetString(body);
                Console.WriteLine(text);
                await Task.CompletedTask;
                _model.BasicAck(ea.DeliveryTag, false);
            };
            _model.BasicConsume(_queueName, false, consumer);
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }
    }
}
