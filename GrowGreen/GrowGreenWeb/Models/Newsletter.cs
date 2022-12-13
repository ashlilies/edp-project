using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Newsletter
    {
        public Newsletter()
        {
            NewsletterEditHistories = new HashSet<NewsletterEditHistory>();
        }

        public int Id { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual ICollection<NewsletterEditHistory> NewsletterEditHistories { get; set; }
    }
}
