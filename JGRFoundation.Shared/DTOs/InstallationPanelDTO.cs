using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JGRFoundation.Shared.DTOs
{
    public class InstallationPanelDTO
    {
        public string installationStrategy { get; set; }
        public decimal demandWatts { get; set; }         
        public int wattsByPanel { get; set; } 
    }
}
