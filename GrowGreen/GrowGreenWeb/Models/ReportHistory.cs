using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class ReportHistory
    {
        public int Id { get; set; }
        public string Filename { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public int AdminId { get; set; }

        public virtual User Admin { get; set; } = null!;
    }
}
