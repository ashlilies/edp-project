using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class PointsEarned
    {
        public int TransactionId { get; set; }
        public int Points { get; set; }
        public int UserId { get; set; }

        public virtual User Transaction { get; set; } = null!;
    }
}
