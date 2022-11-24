using System.ComponentModel.DataAnnotations;

namespace BoardManagementSystem.Models
{
    public class TeloneUserRole
    {
        [Key]
        public int Id { get; set; }
        public long TeloneUserID { get; set; }
        public long TeloneRoleID { get; set; }
    }
}
}
