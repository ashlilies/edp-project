using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Chat
    {
        public Chat()
        {
            ChatReports = new HashSet<ChatReport>();
        }

        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; } = null!;
        public bool IsRead { get; set; }
        public DateTime EditedTimestamp { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<ChatReport> ChatReports { get; set; }
    }
}
