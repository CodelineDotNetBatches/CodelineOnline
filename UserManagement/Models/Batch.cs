using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models
{
   
    public class Batch
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BatchId { get; set; }

        [ForeignKey("Admin_Profiles")]

        public int AdminId { get; set; }

        [ForeignKey("Branch")]
        public int BranchId { get; set; } 

        [Required]
        [MaxLength(100)]
        public string BatchName { get; set; } = default!;

        [Required]
        [MaxLength(50)]
        public string BatchStatus { get; set; } = "Planned"; // e.g., Planned, Ongoing, Completed

        [Required]
        public DateTime BatchStartDate { get; set; }

        [Required]
        public DateTime BatchEndDate { get; set; }

        [MaxLength(256)]
        public string? BatchTimeline { get; set; }

        [MaxLength(500)]
        public string? BatchDescription { get; set; }

        public virtual Branch branchs { get; set; } 

        // navigation property


        public virtual ICollection<Trainee> Trainees { get; set; } = new List<Trainee>(); // one batch can has many trainee

        public virtual Admin_Profile admin_Profile { get; set; } //batch can managed by one admin

        public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>(); // one batch can has many instructor
    } 
}
