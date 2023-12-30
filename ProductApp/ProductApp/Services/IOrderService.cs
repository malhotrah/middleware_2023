using ProductApp.Requests;
using ProductApp.Responses;

namespace ProductApp.Services
{
    public interface IOrderService
    {
        public Task<CreateOrderResponse> CreateOrder(CreateOrderRequest createOrderRequest);
        public Task<UpdateOrderResponse> UpdateOrder(int orderId);

    }
}
