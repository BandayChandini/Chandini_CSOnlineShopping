using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShoppingAPI.Entities
{
    public class Favorite
    {
        [Key]
        
        
        public Guid FavoriteId { get; set; }

        [Required(ErrorMessage = "Id is Required")]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "varchar")]
        [StringLength(10)]
        [ForeignKey("User")]
        public string UserId { get;set; }
        
        
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        [Column(TypeName = "Date")]
        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }
    }
}
