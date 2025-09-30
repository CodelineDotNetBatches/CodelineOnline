using System.ComponentModel.DataAnnotations;

namespace ReportsManagements.Models
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }= true;
    }
}
