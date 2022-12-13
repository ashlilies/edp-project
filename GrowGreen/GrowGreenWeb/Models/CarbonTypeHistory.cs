using System;
using System.Collections.Generic;

namespace GrowGreenWeb.Models
{
    public partial class CarbonTypeHistory
    {
        public int Id { get; set; }
        public double UnitAmount { get; set; }
        public int CarbonHistoryId { get; set; }
        public int CarbonTypeId { get; set; }

        public virtual CarbonHistory CarbonHistory { get; set; } = null!;
        public virtual CarbonType CarbonType { get; set; } = null!;
    }
}
