using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public bool IsRevoked { get; set; }

        public virtual Event Event { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
