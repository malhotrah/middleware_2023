using System.ComponentModel.DataAnnotations;

namespace ProductApp.Requests
{
    public class CreateOrderRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
