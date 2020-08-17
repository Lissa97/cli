using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Table.Models.Tables;
using Table.Models;

namespace Table.Date
{
    public class TableContext: DbContext
    {
        public TableContext(DbContextOptions<TableContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<StudentCourse>()
            .HasOne<Student>(sc => sc.Student)
            .WithMany(s => s.StudentCourses)
            .HasForeignKey(sc => sc.StudentId);


            modelBuilder.Entity<StudentCourse>()
               .HasOne<Cours>(sc => sc.Course)
               .WithMany(s => s.StudentCourses)
               .HasForeignKey(sc => sc.CoursId);

        }

        public DbSet<People> Peoples { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<InfoTable> InfoTables { get; set; }

        public DbSet<RowType> RowTypes { get; set; }

        public DbSet<Table.Models.Tables.Cours> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        public DbSet<Table.Models.Tables.Group> Groups { get; set; }


        

    }
}
