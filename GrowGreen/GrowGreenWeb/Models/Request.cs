using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Request
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public DateTime DateSent { get; set; }
        public bool AcceptedStatus { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual User? Sender { get; set; }
    }
}
