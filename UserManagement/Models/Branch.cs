using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models
{
    public class Branch
    {
        [Key]
      
        public int BranchId { get; set; } // Primary Key for the Branch table

    }
}
