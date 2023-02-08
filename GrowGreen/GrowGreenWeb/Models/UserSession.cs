using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class UserSession
    {
        public int Id { get; set; }
        public string SessionId { get; set; } = null!;
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
