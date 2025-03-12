using System.ComponentModel.DataAnnotations;

namespace LoginSys.Server.Models
{
    public class Users
    {
        [Key]
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "The email address must be valid")]
        public string Email { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 8)]
        public string UserPassword { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
