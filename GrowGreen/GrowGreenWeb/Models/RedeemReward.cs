using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class RedeemReward
    {
        public int RewardId { get; set; }
        public int UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public int PointsDeduction { get; set; }

        public virtual Reward Reward { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
