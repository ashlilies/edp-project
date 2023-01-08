using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Chat
    {
        public Chat()
        {
            ChatReports = new HashSet<ChatReport>();
            InverseReplyToChat = new HashSet<Chat>();
        }

        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; } = null!;
        public bool IsReadByLecturer { get; set; }
        public DateTime EditedTimestamp { get; set; }
        public int UserId { get; set; }
        public int? CourseId { get; set; }
        public int? ReplyToChatId { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Chat? ReplyToChat { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<ChatReport> ChatReports { get; set; }
        public virtual ICollection<Chat> InverseReplyToChat { get; set; }
    }
}
