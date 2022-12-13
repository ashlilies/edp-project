using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class QuizChoice
    {
        public QuizChoice()
        {
            Learners = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public int QuizQuestionId { get; set; }

        public virtual QuizQuestion QuizQuestion { get; set; } = null!;

        public virtual ICollection<User> Learners { get; set; }
    }
}
