using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class BadgeLearner
    {
        public int BadgeId { get; set; }
        public int LearnerId { get; set; }
        public DateTime? Timestamp { get; set; }

        public virtual Badge Badge { get; set; } = null!;
        public virtual User Learner { get; set; } = null!;
    }
}
