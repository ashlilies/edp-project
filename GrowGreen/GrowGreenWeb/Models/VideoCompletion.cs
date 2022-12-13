using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class VideoCompletion
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int LearnerId { get; set; }
        public int VideoId { get; set; }

        public virtual User Learner { get; set; } = null!;
        public virtual Video Video { get; set; } = null!;
    }
}
