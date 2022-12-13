using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class RecyclingLocation
    {
        public RecyclingLocation()
        {
            RecyclingRecords = new HashSet<RecyclingRecord>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }

        public virtual ICollection<RecyclingRecord> RecyclingRecords { get; set; }
    }
}
