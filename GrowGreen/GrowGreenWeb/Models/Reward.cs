using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Reward
    {
        public Reward()
        {
            RedeemRewards = new HashSet<RedeemReward>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Likes { get; set; }
        public string Image { get; set; } = null!;

        public virtual ICollection<RedeemReward> RedeemRewards { get; set; }
    }
}
