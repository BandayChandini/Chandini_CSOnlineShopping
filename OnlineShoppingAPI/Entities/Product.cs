using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShoppingAPI.Entities
{
    public class Product
    {
        [Key]
        
        
        public Guid ProductId {  get; set; }

        [Required(ErrorMessage = "ProductName is Required")]
        [Column(TypeName = "varchar")]
        [StringLength(30)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        public double Price {  get; set; }
        [ForeignKey("Category")]
        
        public Guid CategoryId { get; set; }
        
        
        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string ImageURL { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
