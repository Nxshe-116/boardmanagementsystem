using System.ComponentModel.DataAnnotations;

namespace BoardManagementSystem.Models
{
    public class User
    {
        [Key]
        public long TeloneUserId { get; set; }
        public string Username { get; set; }
        public bool Active { get; set; }

    }
}
