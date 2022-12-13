using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Donation
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Remarks { get; set; } = null!;
        public decimal Amount { get; set; }
        public int ReceipientId { get; set; }
        public string Email { get; set; } = null!;
        public string Purpose { get; set; } = null!;
        public int? SenderId { get; set; }
        public int? TransactionId { get; set; }

        public virtual User? Sender { get; set; }
        public virtual Payment? Transaction { get; set; }
    }
}
