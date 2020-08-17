using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Table.Date;
using Table.Models.Tables;
using Table.Models;

namespace Table.Controllers
{
    [Route("api/StudentsCourses")]
    [ApiController]
    public class StudentCoursesController : ControllerBase
    {
        private readonly TableContext _context;

        public StudentCoursesController(TableContext context)
        {
            _context = context;
        }
        private class StudentCourseTable
        {
            public int id { get; set; }
            public int dogovor { get; set; }
            public string student { get; set; }
            public string course { get; set; }
        }

        // GET: api/StudentCourses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCourse>>> GetStudentCourses()
        {
            List<StudentCourse> listT = await _context.StudentCourses.OrderBy(s => s.Dogovor).ToListAsync();

            int n = listT.Count();
            //int m = 2;

            List<StudentCourseTable> arrT = new List<StudentCourseTable>();

            for (int i = 0; i < n; i++)
            {
                Student student = await _context.Students.FindAsync(listT[i].StudentId);
                Cours cours = await _context.Courses.FindAsync(listT[i].CoursId);

                StudentCourseTable item = new StudentCourseTable { id = listT[i].Id, dogovor = listT[i].Dogovor, student = student.Name, course = cours.name };
                arrT.Add(item);
            }

            return new ObjectResult(arrT);
           // return await _context.StudentCourses.OrderBy(s => s.Dogovor).ToListAsync();

        }
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetOpt()
        {
            List<StudentCourse> listT = await _context.StudentCourses.OrderBy(s => s.Dogovor).ToListAsync();

            int n = listT.Count();

            List<string[]> arrT = new List<string[]>();

            for (int i = 0; i < n; i++)
            {

                string[] item = { listT[i].Id.ToString(), listT[i].Dogovor.ToString() };

                arrT.Add(item);
            }

            return new ObjectResult(arrT);

        }
        public async Task<List<StudentCourse>> SortStudentCours(IQueryable<StudentCourse> h, string sort, bool reverse)
        {
            if (sort == "Student")
            {
                if (reverse)
                    return await h.OrderByDescending(s => s.StudentId).ToListAsync();
                else
                    return await h.OrderBy(s => s.StudentId).ToListAsync();
            }

            else if (sort == "Cours")
            {
                if (reverse)
                    return await h.OrderByDescending(s => s.CoursId).ToListAsync();
                else
                    return await h.OrderBy(s => s.CoursId).ToListAsync();
            }
            else if (reverse)
                return await h.OrderByDescending(s => s.Dogovor).ToListAsync();
            else
               return await h.OrderBy(s => s.Dogovor).ToListAsync();
        }

        // GET: api/StudentCourses/5
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<StudentCourse>> PostSort([FromBody] Body b)
        {
            IQueryable<StudentCourse> h = _context.StudentCourses;
            List<StudentCourse> listT = await SortStudentCours(h, b.sort, b.reverse);
           

            int n = listT.Count();
            //int m = 2;

            List<String[]> arrT = new List<String[]>();

            for (int i = 0; i < n; i++)
            {
                Student student = await _context.Students.FindAsync(listT[i].StudentId);
                Cours cours = await _context.Courses.FindAsync(listT[i].CoursId);

                String[] item = { listT[i].Id.ToString(), listT[i].Dogovor.ToString(), student.Name, cours.name };
                arrT.Add(item);
            }

            return new ObjectResult(arrT);
        }

        // PUT: api/StudentCourses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<ActionResult<int>> PutStudentCourse(String[] arrT)
        {
            Student student = await _context.Students.FindAsync(Int32.Parse(arrT[2]));
            Cours cours = await _context.Courses.FindAsync(Int32.Parse(arrT[3]));

            StudentCourse studentCourse = new StudentCourse { Id = Int32.Parse(arrT[0]), Dogovor = Int32.Parse(arrT[1]), StudentId = Int32.Parse(arrT[2]), Student = student, CoursId = Int32.Parse(arrT[3]), Course = cours };
            _context.StudentCourses.Add(studentCourse);


            _context.Entry(studentCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentCourseExists(studentCourse.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return studentCourse.Id;
        }

        // POST: api/StudentCourses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<int>> Post(String[] arrT)
        {
            Student student = await _context.Students.FindAsync(Int32.Parse(arrT[1]));
            Cours cours = await _context.Courses.FindAsync(Int32.Parse(arrT[2]));

            StudentCourse studentCourse = new StudentCourse { Dogovor = Int32.Parse(arrT[0]), StudentId = Int32.Parse(arrT[1]), Student = student, CoursId = Int32.Parse(arrT[2]), Course = cours };
            _context.StudentCourses.Add(studentCourse);

            await _context.SaveChangesAsync();


            return studentCourse.Id;
        }

        // DELETE: api/StudentCourses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteStudentCourse(int id)
        {
            var studentCourse = await _context.StudentCourses.FindAsync(id);
            if (studentCourse == null)
            {
                return NotFound();
            }

            _context.StudentCourses.Remove(studentCourse);
            await _context.SaveChangesAsync();

            return studentCourse.Id;
        }

        private bool StudentCourseExists(int id)
        {
            return _context.StudentCourses.Any(e => e.StudentId == id);
        }
    }
}
