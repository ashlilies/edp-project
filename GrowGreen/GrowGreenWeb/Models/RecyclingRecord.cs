using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class RecyclingRecord
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int RecyclingLocationId { get; set; }
        public int ItemTypeId { get; set; }
        public int LearnerId { get; set; }

        public virtual ItemType ItemType { get; set; } = null!;
        public virtual User Learner { get; set; } = null!;
        public virtual RecyclingLocation RecyclingLocation { get; set; } = null!;
    }
}
