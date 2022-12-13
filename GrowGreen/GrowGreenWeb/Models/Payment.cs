using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Donations = new HashSet<Donation>();
            Tips = new HashSet<Tip>();
        }

        public int TransactionId { get; set; }
        public string TransactionType { get; set; } = null!;
        public string Remarks { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CardDetails { get; set; } = null!;
        public string FullName { get; set; } = null!;

        public virtual ICollection<Donation> Donations { get; set; }
        public virtual ICollection<Tip> Tips { get; set; }
    }
}
