namespace ReportsManagements.DTOs
{
    public class BranchDtos
    {
        public class BranchCreateDto
        {
            public string Name { get; set; }
        }

        public class BranchResponseDto
        {
            public int BranchId { get; set; }
            public string Name { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
