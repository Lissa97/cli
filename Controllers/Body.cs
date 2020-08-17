using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Table.Controllers
{
    public class Body
    {
        public string sort { get; set; }
        public bool reverse { get; set; }
        public string[] filter { get; set; }
    }
}
