using System.ComponentModel.DataAnnotations;

namespace BoardManagementSystem.Models
{
    public class DocumentDetail
    {
        [Key]
        public long Id { get; set; }
        public String DocumentName { get; set; }
        public String DocumentDescription { get; set; }

        public String Displayname { get; set; }

        public DateTime UplodadedOn { get; set; }
    }
}
