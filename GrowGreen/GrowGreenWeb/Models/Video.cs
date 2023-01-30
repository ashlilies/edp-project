using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Video
    {
        public Video()
        {
            VideoCompletions = new HashSet<VideoCompletion>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public string Transcript { get; set; } = null!;
        public int LectureId { get; set; }
        public string Url { get; set; } = null!;

        public virtual Lecture Lecture { get; set; } = null!;
        public virtual ICollection<VideoCompletion> VideoCompletions { get; set; }
    }
}
