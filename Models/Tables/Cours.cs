using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Table.Models.Tables
{
    public class Cours
    {
        public int id { get; set; }
        public string name { get; set; }
        public int count_in_week { get; set; }
        public IList<StudentCourse> StudentCourses { get; set; }

    }
}
