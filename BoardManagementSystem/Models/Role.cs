using System.ComponentModel.DataAnnotations;

namespace BoardManagementSystem.Models
{
    public class Role
    {
        [Key]
        public long TeloneRoleID { get; set; }
        public string role { get; set; }
    }
}
