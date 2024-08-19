using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShoppingAPI.Entities
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemId { get; set; }
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        
        
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "Price is Required")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Quantity is Required")]
        public int Quantity { get; set; }


        [JsonIgnore]
        
        public Product? Product { get; set; }
    }
}
