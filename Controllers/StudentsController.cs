using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Table.Date;
using Table.Models.Tables;

namespace Table.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly TableContext _context;

        public StudentsController(TableContext context)
        {
            _context = context;
        }

        private class CoursTable
        {
            public int id { get; set; }
            public string name { get; set; }
            public int count_in_week { get; set; }
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            List<Student> listT = await _context.Students.OrderBy(s => s.Name).ToListAsync();

            int n = listT.Count();
            //int m = 2;

            List<CoursTable> arrT = new List<CoursTable>();

            for (int i = 0; i < n; i++)
            {
                CoursTable item = new CoursTable {id = listT[i].Id, name = listT[i].Name};
                arrT.Add(item);
            }

            return new ObjectResult(arrT);
            //return await _context.Students.OrderBy(s => s.Name).ToListAsync();
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetOpt()
        {
            List<Student> listT = await _context.Students.OrderBy(s => s.Name).ToListAsync();

            int n = listT.Count();

            List<string[]> arrT = new List<string[]>();

            for (int i = 0; i < n; i++)
            {

                string[] item = { listT[i].Id.ToString(), listT[i].Name };

                arrT.Add(item);
            }

            return new ObjectResult(arrT);

        }
        public async Task<List<Student>> SortStudentCours(IQueryable<Student> h, string sort, bool reverse)
        {
            if (reverse)
                return await h.OrderByDescending(s => s.Name).ToListAsync();
            else
                return await h.OrderBy(s => s.Name).ToListAsync();
          
        }
        // GET: api/Students/5
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Student>> PostSort([FromBody] Body b)
        {

            IQueryable<Student> h = _context.Students; 
            List<Student> listT = await SortStudentCours(h, b.sort, b.reverse);

            int n = listT.Count();
            //int m = 2;

            List<String[]> arrT = new List<String[]>();

            for (int i = 0; i < n; i++)
            {
                String[] item = { listT[i].Id.ToString(), listT[i].Name };
                arrT.Add(item);
            }

            return new ObjectResult(arrT);
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<ActionResult<int>> PutStudent(String[] arrT)
        {
            Student student = new Student { Id = Int32.Parse(arrT[0]), Name = arrT[1] };


            _context.Update(student);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return student.Id;
        }

        // POST: api/Students
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<int>> Post(String[] arrT)
        {
            Student student = new Student {  Name = arrT[0] };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return student.Id;
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return id;
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
