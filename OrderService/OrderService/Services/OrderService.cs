using Grpc.Core;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace OrderApp.Services
{
    public class OrderService : Order.OrderBase
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IRabbitMqService _rabbitMqService;

        public OrderService(ILogger<OrderService> logger, IRabbitMqService rabbitMqService)
        {
            _logger = logger;
            _rabbitMqService = rabbitMqService;
        }

        public override Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request, 
            ServerCallContext context)
        {
            var orderId = new Random().Next();
            var orderRes = new CreateOrderResponse
            {
                OrderId = orderId
            };

            //throw rabbit mq fanout event
            try
            {
                using var connection = _rabbitMqService.CreateChannel();
                using var model = connection.CreateModel();
                //model.ExchangeDeclare("fanout.exchange", ExchangeType.Fanout, true);
                var json = JsonConvert.SerializeObject(orderRes);
                var body = Encoding.UTF8.GetBytes(json);
                model.BasicPublish("fanout_exchange",
                                     string.Empty,
                                     basicProperties: null,
                                     body: body);
                return Task.FromResult(orderRes);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public override Task<UpdateOrderResponse> UpdateOrder(UpdateOrderRequest request,
            ServerCallContext context)
        {
            var orderId = request.OrderId;
            var orderRes = new UpdateOrderResponse
            {
                OrderId = orderId,
                Status = request.Status,
            };

            //throw rabbit mq fanout event
            try
            {
                using var connection = _rabbitMqService.CreateChannel();
                using var model = connection.CreateModel();
                var json = JsonConvert.SerializeObject(orderRes);
                var body = Encoding.UTF8.GetBytes(json);
                model.BasicPublish("topic_exchange",
                                     "order.ns2.updated",
                                     basicProperties: null,
                                     body: body);
                return Task.FromResult(orderRes);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}