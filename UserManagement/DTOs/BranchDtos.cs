using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.DTOs
{
    public class BranchDTO
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }

        public ICollection<int> PhoneNumbers { get; set; } // List of phone numbers associated with the branch
    }
}
