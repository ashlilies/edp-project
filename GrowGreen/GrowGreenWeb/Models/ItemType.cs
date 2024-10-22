﻿using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class ItemType
    {
        public ItemType()
        {
            RecyclingRecords = new HashSet<RecyclingRecord>();
            RecyclingLocations = new HashSet<RecyclingLocation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<RecyclingRecord> RecyclingRecords { get; set; }

        public virtual ICollection<RecyclingLocation> RecyclingLocations { get; set; }
    }
}
