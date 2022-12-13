using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class GivingReview
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public string Content { get; set; } = null!;
        public int SenderId { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
