using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingAPI.Entities
{
    public class Category
    {
        [Key]
        
        [Required(ErrorMessage ="Id is required")]
        public Guid CategoryId { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(100)]
        

        public string? CategoryName { get; set; }
        public string? Description { get; set; }

    }
}
