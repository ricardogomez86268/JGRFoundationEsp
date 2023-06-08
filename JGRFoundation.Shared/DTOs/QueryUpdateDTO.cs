using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGRFoundation.Shared.DTOs
{
    public class QueryUpdateDTO
    {
        public List<string> columns { get; set; }
        public string condition { get; set; }
    }
}
