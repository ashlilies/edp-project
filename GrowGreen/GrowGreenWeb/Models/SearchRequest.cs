using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class SearchRequest
    {
        public int Id { get; set; }
        public string Location { get; set; } = null!;
        public int Popularity { get; set; }
        public string Keyword { get; set; } = null!;
        public DateTime Date { get; set; }
        public int? UserId { get; set; }
        public int? SearchResultId { get; set; }

        public virtual SearchResult? SearchResult { get; set; }
        public virtual User? User { get; set; }
    }
}
