using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Table.Models.Tables
{
    public class People
    {
        public int Id { get; set; }
        public string Family_name { get; set; }
        public string Name { get; set; }
        public string Fathers_name { get; set; }

    }
    public class Teacher : People
    {
        public string Description {get; set;}
    }
    public class Student : People
    {
        public IList<StudentCourse> StudentCourses { get; set; }
    }

}
