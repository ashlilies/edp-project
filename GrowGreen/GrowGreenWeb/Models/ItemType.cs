using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class ItemType
    {
        public ItemType()
        {
            RecyclingRecords = new HashSet<RecyclingRecord>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<RecyclingRecord> RecyclingRecords { get; set; }
    }
}
