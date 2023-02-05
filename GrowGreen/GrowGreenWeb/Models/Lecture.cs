using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Lecture
    {
        public Lecture()
        {
            Quizzes = new HashSet<Quiz>();
            Videos = new HashSet<Video>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public int CourseId { get; set; }
        public DateTime StartDate { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual ICollection<Quiz> Quizzes { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}
