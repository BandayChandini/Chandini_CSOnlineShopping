using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShoppingAPI.Entities
{
    public class CartItem
    {
        [Key]
        
        public Guid CartItemId { get; set; }
        
        [ForeignKey("Product")]
        
       
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "Price is Required")]
        
        public double Price { get; set; }
        
        [Required(ErrorMessage = "Quantity is Required")]
        public int Quantity { get; set; }


        [Required(ErrorMessage = "TotalPrice is Required")]

        public double TotalPrice { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
        

    }
}
