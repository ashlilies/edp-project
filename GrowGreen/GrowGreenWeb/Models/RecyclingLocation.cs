using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class RecyclingLocation
    {
        public RecyclingLocation()
        {
            RecyclingRecords = new HashSet<RecyclingRecord>();
            ItemTypes = new HashSet<ItemType>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public string Address { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<RecyclingRecord> RecyclingRecords { get; set; }

        public virtual ICollection<ItemType> ItemTypes { get; set; }
    }
}
