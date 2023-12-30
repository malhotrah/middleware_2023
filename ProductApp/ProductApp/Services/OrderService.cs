using Grpc.Net.Client;
using OrderApp;
using ProductApp.Requests;
using ProductApp.Responses;
using CreateOrderRequest = OrderApp.CreateOrderRequest;

namespace ProductApp.Services
{
    public class OrderService : IOrderService
    {
        public OrderService()
        {
            
        }

        public async Task<Responses.CreateOrderResponse> CreateOrder(Requests.CreateOrderRequest createOrderRequest)
        {
            //need to call order service here
            var target = "https://localhost:7057";
            var channel = GrpcChannel.ForAddress(target);
            var client = new Order.OrderClient(channel);
            var orderRes = await client.CreateOrderAsync(new CreateOrderRequest { 
                ProductId = createOrderRequest.ProductId, 
                Amount = (double) createOrderRequest.Price, 
                Quantity = createOrderRequest.Quantity
            });

            return new Responses.CreateOrderResponse()
            {
                OrderId = orderRes.OrderId
            };
        }

        public async Task<Responses.UpdateOrderResponse> UpdateOrder (int orderId)
        {
            //need to call order service here
            var target = "https://localhost:7057";
            var channel = GrpcChannel.ForAddress(target);
            var client = new Order.OrderClient(channel);
            var orderRes = await client.UpdateOrderAsync(new UpdateOrderRequest
            {
                OrderId = orderId,
                Status = "Completed"
            });

            return new Responses.UpdateOrderResponse()
            {
                OrderId = orderRes.OrderId,
                Status = orderRes.Status
            };
        }
    }
}
