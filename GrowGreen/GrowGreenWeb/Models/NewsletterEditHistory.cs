using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class NewsletterEditHistory
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int NewsletterId { get; set; }
        public int UserId { get; set; }

        public virtual Newsletter Newsletter { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
