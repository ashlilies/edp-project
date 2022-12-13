using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class CarbonType
    {
        public CarbonType()
        {
            CarbonTypeHistories = new HashSet<CarbonTypeHistory>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string UnitName { get; set; } = null!;
        public double TonsPerUnit { get; set; }

        public virtual ICollection<CarbonTypeHistory> CarbonTypeHistories { get; set; }
    }
}
