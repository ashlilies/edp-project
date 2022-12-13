using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Event
    {
        public Event()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public int MaxParticipants { get; set; }
        public string Attendee { get; set; } = null!;
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public string SpecialGuest { get; set; } = null!;
        public bool ActiveStatus { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
