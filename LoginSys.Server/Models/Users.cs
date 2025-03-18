using System.ComponentModel.DataAnnotations;

namespace LoginSys.Server.Models
{
    public class Users
    {
        [Key]
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "The password needs to be at least 8 characters long", MinimumLength = 8)]
        public string UserPassword { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
