using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class ChatReport
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
        public int ChatId { get; set; }

        public virtual Chat Chat { get; set; } = null!;
    }
}
