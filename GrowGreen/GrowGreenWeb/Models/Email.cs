using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Email
    {
        public Email()
        {
            Newsletters = new HashSet<Newsletter>();
        }

        public int Id { get; set; }
        public string Email1 { get; set; } = null!;

        public virtual ICollection<Newsletter> Newsletters { get; set; }
    }
}
