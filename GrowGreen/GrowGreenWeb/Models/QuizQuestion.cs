using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class QuizQuestion
    {
        public QuizQuestion()
        {
            QuizChoices = new HashSet<QuizChoice>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Points { get; set; }
        public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; } = null!;
        public virtual ICollection<QuizChoice> QuizChoices { get; set; }
    }
}
