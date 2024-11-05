using System.ComponentModel.DataAnnotations;

namespace LoginSys.Server.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserPassword { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
