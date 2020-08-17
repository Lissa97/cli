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
    [Route("api/Teachers")]
    [ApiController]
    public class APITeachersController : ControllerBase
    {
        TableContext db;
        public APITeachersController(TableContext context)
        {
            db = context;
            if (!db.Teachers.Any()) //db.SomeEntityCollection.Count()
            {
                db.Teachers.Add(new Teacher { Name = "Tom", Description = "ygfhgfugu" });
                db.Teachers.Add(new Teacher { Name = "Alice", Description = "fgiuhuh" });
                db.SaveChanges();
            }
        }
        private class TeacherTable
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> Get()
        {
            List<Teacher> listT = await db.Teachers.OrderBy(s => s.Name).ToListAsync();

            int n = listT.Count();

            List<TeacherTable> arrT = new List<TeacherTable>();

            for(int i=0; i<n; i++)
            {
                TeacherTable item = new TeacherTable { id = listT[i].Id, name = listT[i].Name, description = listT[i].Description };
                arrT.Add(item);
            }

            return new ObjectResult(arrT);
            //return await db.Teachers.ToListAsync();
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetOpt()
        {
            List<Teacher> listT = await db.Teachers.OrderBy(s => s.Name).ToListAsync();

            int n = listT.Count();

            List<string[]> arrT = new List<string[]>();

            for (int i = 0; i < n; i++)
            {

                string[] item = { listT[i].Id.ToString(), listT[i].Name };

                arrT.Add(item);
            }

            return new ObjectResult(arrT);

        }
        public async Task<List<Teacher>> SortGroup(IQueryable<Teacher> h, string sort, bool reverse)
        {
            if (sort == "Description")
            {
                if (reverse)
                    return await db.Teachers.OrderByDescending(s => s.Description).ToListAsync();
                else
                    return await db.Teachers.OrderBy(s => s.Description).ToListAsync();
            }
            else if (reverse)
                return await db.Teachers.OrderByDescending(s => s.Name).ToListAsync();

            else
                return await h.OrderBy(s => s.Name).ToListAsync();
        }
        // GET api/teachers/5
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostSort([FromBody] Body b)
        {
            var h = db.Teachers;
            List<Teacher> listT = await SortGroup(h, b.sort, b.reverse); 

            int n = listT.Count();
            //int m = 2;

            List<String[]> arrT = new List<String[]>();

            for (int i = 0; i < n; i++)
            {
                String[] item = { listT[i].Id.ToString(), listT[i].Name, listT[i].Description };
                arrT.Add(item);
            }

            return new ObjectResult(arrT);
        }

        // POST api/teachers
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<int>> Post(String[] arrT)
        {
            Teacher teacher = new Teacher { Name = arrT[0], Description = arrT[1] };
            if (teacher == null)
            {
                return BadRequest();
            }

            db.Teachers.Add(teacher);
            await db.SaveChangesAsync();
            return teacher.Id;
        }

        // PUT api/teachers/
        [HttpPut]
        public async Task<ActionResult<int>> Put(String[] arrT)
        {
            Teacher teacher = new Teacher { Id = Int32.Parse(arrT[0]), Name = arrT[1], Description = arrT[2] };

            if (teacher == null)
            {
                return BadRequest();
            }
            if (!db.Teachers.Any(x => x.Id == teacher.Id))
            {
                return NotFound();
            }

            db.Update(teacher);
            await db.SaveChangesAsync();
            return Ok(teacher.Id);
        }

        // DELETE api/teachers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            Teacher teacher = db.Teachers.FirstOrDefault(x => x.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            db.Teachers.Remove(teacher);
            await db.SaveChangesAsync();
            return id;
        }
    }
}