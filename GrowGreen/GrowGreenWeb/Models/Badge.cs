using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Badge
    {
        public Badge()
        {
            BadgeLearners = new HashSet<BadgeLearner>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CourseId { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual ICollection<BadgeLearner> BadgeLearners { get; set; }
    }
}
