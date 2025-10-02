using UserManagement.Models;
//using static UserManagement.SeedData.InstructorsSeedData;
namespace UserManagement.SeedData
{
    public class AvailabilitiesSeedData
    {
        public static void AvailabilitiesSeed(Microsoft.EntityFrameworkCore.ModelBuilder mb)
        {

            mb.Entity<Availability>().HasData(
                new Availability
                {
                    InstructorId = 101,
                    avilabilityId = 1,
                    day_of_week = DaysOfWeek.Monday,
                    Avail_Status = AvailabilityStatus.Active,
                    time = new TimeOnly(9, 0) // 9:00 AM


                },
                new Availability
                {
                    InstructorId = 101,
                    avilabilityId = 2,
                    day_of_week = DaysOfWeek.Wednesday,
                    Avail_Status = AvailabilityStatus.Busy,
                    time = new TimeOnly(14, 0) // 2:00 PM

                },
                new Availability
                {
                    InstructorId = 102,
                    avilabilityId = 3,
                    day_of_week = DaysOfWeek.Tuesday,
                    Avail_Status = AvailabilityStatus.Inactive,
                    time = new TimeOnly(11, 0) // 11:00 AM

                },
                new Availability
                {
                    InstructorId = 102,
                    avilabilityId = 4,
                    day_of_week = DaysOfWeek.Thursday,
                    Avail_Status = AvailabilityStatus.Completed,
                    time = new TimeOnly(16, 0) // 4:00 PM

                },
                new Availability
                {
                    InstructorId = 103,
                    avilabilityId = 5,
                    day_of_week = DaysOfWeek.Friday,
                    Avail_Status = AvailabilityStatus.Active,
                    time = new TimeOnly(10, 0) // 10:00 AM

                },
                new Availability
                {
                    InstructorId = 104,
                    avilabilityId = 6,
                    day_of_week = DaysOfWeek.Saturday,
                    Avail_Status = AvailabilityStatus.Busy,
                    time = new TimeOnly(13, 0) // 1:00 PM


                }
                );
        }

    }
}
