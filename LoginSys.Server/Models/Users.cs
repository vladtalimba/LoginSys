using System.ComponentModel.DataAnnotations;

namespace LoginSys.Server.Models
{
    public class Users
    {
        [Key]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserPassword { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
