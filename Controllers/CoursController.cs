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
    [Route("api/Courses")]
    [ApiController]
    public class CoursController : ControllerBase
    {
        private readonly TableContext _context;

        public CoursController(TableContext context)
        {
            _context = context;
            int c = 2;
            if (_context.Courses.Count() < c) //db.SomeEntityCollection.Count()
            {
                _context.Courses.Add(new Cours { name = "Ракетостроение", count_in_week = 2 });
                _context.Courses.Add(new Cours { name = "Программирование", count_in_week = 1});
                _context.SaveChanges();
            }
        }
        private class CoursTable
        {
            public int id { get; set; }
            public string name { get; set; }
            public int count_in_week { get; set; }
        }

        // GET: api/Cours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cours>>> GetCours()
        {
            List<Cours> listT = await _context.Courses.OrderBy(s => s.name).ToListAsync();

            int n = listT.Count();

            List<CoursTable> arrT = new List<CoursTable>();

            for (int i = 0; i < n; i++)
            {
                CoursTable item = new CoursTable { id = listT[i].id, name = listT[i].name, count_in_week = listT[i].count_in_week };
                arrT.Add(item);
            }

            return new ObjectResult(arrT);
            //return await _context.Courses.ToListAsync();
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetOpt()
        {
            List<Cours> listT = await _context.Courses.OrderBy(s => s.name).ToListAsync();

            int n = listT.Count();

            List<string[]> arrT = new List<string[]>();

            for (int i = 0; i < n; i++)
            {

                string[] item = { listT[i].id.ToString(), listT[i].name };

                arrT.Add(item);
            }

            return new ObjectResult(arrT);

        }
        public async Task<List<Cours>> SortGroup(IQueryable<Cours> h, string sort, bool reverse)
        {
            if (sort == "count_in_week")
            {
                if (reverse)
                    return await h.OrderByDescending(s => s.count_in_week).ToListAsync();
                else
                    return await h.OrderBy(s => s.count_in_week).ToListAsync();
            }

            else if (reverse)
                return await h.OrderByDescending(s => s.name).ToListAsync();

            else
                return await h.OrderBy(s => s.name).ToListAsync();
        }
        // GET: api/Cours/5
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Cours>> PostSort([FromBody] Body b)
        {

            var h = _context.Courses;
            List<Cours> listT = await SortGroup(h, b.sort, b.reverse);

            int n = listT.Count();

            List<String[]> arrT = new List<String[]>();

            for (int i = 0; i < n; i++)
            {
                String[] item = { listT[i].id.ToString(), listT[i].name, listT[i].count_in_week.ToString() };
                arrT.Add(item);
            }

            return new ObjectResult(arrT);
            //return await _context.Courses.ToListAsync();
        }

        // PUT: api/Cours/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<ActionResult<int>> PutCours(string[] arrT)
        {
            Cours cours = new Cours {id = Int32.Parse(arrT[0]), name = arrT[1], count_in_week = Int32.Parse(arrT[2]) };


            _context.Entry(cours).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoursExists(cours.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(cours.id);
        }

        // POST: api/Cours
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<int>> Post(string[] arrT)
        {
            Cours cours = new Cours {name = arrT[0], count_in_week = Int32.Parse(arrT[1]) };
            
            _context.Courses.Add(cours);
            await _context.SaveChangesAsync();

            return cours.id;
        }

        // DELETE: api/Cours/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteCours(int id)
        {
            var cours = await _context.Courses.FindAsync(id);
            if (cours == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(cours);
            await _context.SaveChangesAsync();

            return cours.id;
        }

        private bool CoursExists(int id)
        {
            return _context.Courses.Any(e => e.id == id);
        }
    }
}
