using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Table.Models.Tables
{
    public class StudentCourse
    {
        public int Id { get; set; }
        public int Dogovor { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CoursId { get; set; }
        public Cours Course { get; set; }
    }
}
