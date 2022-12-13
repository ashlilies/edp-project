using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Tip
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public int CompletedId { get; set; }
        public int TransactionId { get; set; }

        public virtual Completed Completed { get; set; } = null!;
        public virtual Payment Transaction { get; set; } = null!;
    }
}
