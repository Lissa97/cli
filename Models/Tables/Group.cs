using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Table.Models.Tables
{
    public class Group
    {
        public int id { get; set; }
        public string name { get; set; }


        public int id_cours { get; set; }
        public int id_teacher { get; set; }
    }
}
