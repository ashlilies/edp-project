﻿using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Course
    {
        public Course()
        {
            Badges = new HashSet<Badge>();
            CourseReviews = new HashSet<CourseReview>();
            Lectures = new HashSet<Lecture>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<Badge> Badges { get; set; }
        public virtual ICollection<CourseReview> CourseReviews { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
    }
}