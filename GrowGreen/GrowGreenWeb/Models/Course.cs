using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Course
    {
        public Course()
        {
            Badges = new HashSet<Badge>();
            Chats = new HashSet<Chat>();
            CourseReviews = new HashSet<CourseReview>();
            CourseSignups = new HashSet<CourseSignup>();
            Lectures = new HashSet<Lecture>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxCapacity { get; set; }
        public int LecturerId { get; set; }
        public DateTime? LastUpdatedTimestamp { get; set; }
        public string? ImageUrl { get; set; }

        public virtual User Lecturer { get; set; } = null!;
        public virtual ICollection<Badge> Badges { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<CourseReview> CourseReviews { get; set; }
        public virtual ICollection<CourseSignup> CourseSignups { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
    }
}
