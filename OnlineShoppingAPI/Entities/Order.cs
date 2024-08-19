using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineShoppingAPI.Entities
{
    public class Order
    {
        [Key]
        
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid OrderId {  get; set; }
        
        [Column(TypeName = "varchar")]
        [StringLength(100)]
        [Required(ErrorMessage = "Address is Required")]
        public string Address {  get; set; }
        
        [Required(ErrorMessage = "Price is Required")]
        public double price {  get; set; }
        public double Totalprice { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(100)]
        [Required(ErrorMessage = "OrderStatus is Required")]
        public string OrderStatus {  get; set; }
        [Column(TypeName ="Date")]
        [Required(ErrorMessage = "DeliveryDate is Required")]
        public DateTime DeliveryDate { get; set; }
        [Column(TypeName = "Date")]
        [Required(ErrorMessage = "OrderDate is Required")]
        
        public DateTime OrderDate { get; set; }
        
        
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }
        [ForeignKey("User")]
        [Required(ErrorMessage = "Id is Required")]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "varchar")]
        [StringLength(10)]

        public string UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        
    }
}
