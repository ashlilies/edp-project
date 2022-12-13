using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class CarbonHistory
    {
        public CarbonHistory()
        {
            CarbonTypeHistories = new HashSet<CarbonTypeHistory>();
        }

        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int LearnerId { get; set; }

        public virtual User Learner { get; set; } = null!;
        public virtual ICollection<CarbonTypeHistory> CarbonTypeHistories { get; set; }
    }
}
