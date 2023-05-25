using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGRFoundation.Shared.DTOs
{
    public class TemporalHomeDTO
    {
        public int Id { get; set; }

        public int ApplianceId { get; set; }

        public float Quantity { get; set; } = 1;
    }
}
