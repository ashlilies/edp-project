using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class CourseSignup
    {
        public int LearnerId { get; set; }
        public int CourseId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime Timestamp { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual User Learner { get; set; } = null!;
    }
}
