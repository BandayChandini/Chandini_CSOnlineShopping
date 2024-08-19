using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShoppingAPI.Entities
{
    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; }

        [ForeignKey("Order")]
        
        public Guid OrderId { get; set; }
        
        [ForeignKey("User")]
        [Required(ErrorMessage = "Id is Required")]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "varchar")]
        [StringLength(10)]

        public string UserId { get; set; }
        [Required(ErrorMessage = "TransactionMethod is required")]
        [StringLength(50, ErrorMessage = "Transaction method can't exceed 50 characters")]
        public string TransactionMethod { get; set; }

        [Required(ErrorMessage = "TransactionStatus is required")]
        [StringLength(50, ErrorMessage = "Transaction status can't exceed 50 characters")]
        public string TransactionStatus { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public Order? Order { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        

    }
}
