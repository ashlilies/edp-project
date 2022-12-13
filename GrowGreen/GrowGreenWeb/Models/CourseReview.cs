using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class CourseReview
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; } = null!;
    }
}
