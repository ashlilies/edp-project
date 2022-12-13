using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class SearchResult
    {
        public SearchResult()
        {
            SearchRequests = new HashSet<SearchRequest>();
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public int MatchedValue { get; set; }
        public double MatchedPercentage { get; set; }
        public int? UserId { get; set; }

        public virtual ICollection<SearchRequest> SearchRequests { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
