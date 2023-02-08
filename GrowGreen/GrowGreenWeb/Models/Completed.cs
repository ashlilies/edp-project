using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Completed
    {
        public Completed()
        {
            Tips = new HashSet<Tip>();
        }

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int PostId { get; set; }

        public virtual ICollection<Tip> Tips { get; set; }
    }
}
