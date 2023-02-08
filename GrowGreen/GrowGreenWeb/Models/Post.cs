using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class Post
    {
        public Post()
        {
            GivingReviews = new HashSet<GivingReview>();
            Requests = new HashSet<Request>();
            SearchResults = new HashSet<SearchResult>();
        }

        public int PostId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; } = null!;
        public string Title { get; set; } = null!;
        public int Likes { get; set; }
        public string Image { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<GivingReview> GivingReviews { get; set; }
        public virtual ICollection<Request> Requests { get; set; }

        public virtual ICollection<SearchResult> SearchResults { get; set; }
    }
}
