using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShoppingAPI.Entities
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "Id is Required")]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "varchar")]
        [StringLength(10)]
        
        public string UserId {  get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [Column(TypeName = "varchar")]
        [StringLength(30)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Invalid EmailId")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Pls Enter Password")]
        [RegularExpression("[A-Za-z0-9]{6,8}", ErrorMessage = "Password allow only 6 to 8 chars")]
        public string Password { get; set; }
         
        
        [Required(ErrorMessage = "Role is Required")]
        [Column(TypeName = "varchar")]
        [StringLength(20)]
        public string Role { get; set; }
        [Required(ErrorMessage = "Pls Enter Mobile")]
        [RegularExpression("[6-9][0-9]{9}", ErrorMessage = "Invalid Mobile No")]
        public string Mobile { get; set; }

    }
}
