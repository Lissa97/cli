using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Table.Date;
using Table.Models.Tables;
using Table.Models;

namespace Table.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly TableContext _context;

        public GroupsController(TableContext context)
        {
            _context = context;

        }
        private class GroupTable
        {
            public int id { get; set; }
            public string name { get; set; }
            public string cours { get; set; }
            public string teacher { get; set; }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroup()
        {
            List<Group> listT = await _context.Groups.OrderBy(s => s.name).ToListAsync();

            int n = listT.Count();

            List<GroupTable> arrT = new List<GroupTable>();

            for (int i = 0; i < n; i++)
            {
                var cours = await _context.Courses.FindAsync(listT[i].id_cours);
                var teacher = await _context.Teachers.FindAsync(listT[i].id_teacher);
                string cname = "";

                if (cours != null)
                    cname = cours.name;

                GroupTable item = new GroupTable { id = listT[i].id, name = listT[i].name, cours = cname, teacher = teacher.Name };

                arrT.Add(item);
            }

            return new ObjectResult(arrT);
           //  return await _context.Groups.ToListAsync();
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetOpt()
        {
            List<Group> listT = await _context.Groups.OrderBy(s => s.name).ToListAsync();

            int n = listT.Count();

            List<string[]> arrT = new List<string[]>();

            for (int i = 0; i < n; i++)
            {

                string[] item = {listT[i].id.ToString(), listT[i].name};

                arrT.Add(item);
            }

            return new ObjectResult(arrT);
           
        }


        // PUT: api/Groups/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        
        public async Task<ActionResult<int>> PutGroup(string[] arrT)
        {
            var cours = await _context.Courses.FindAsync(Int32.Parse(arrT[2]));
            var teacher = await _context.Teachers.FindAsync(Int32.Parse(arrT[3]));

            Group @group = new Group { id = Int32.Parse(arrT[0]), name = arrT[1], id_cours = cours.id, id_teacher = teacher.Id };


            _context.Entry(@group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(@group.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(@group.id);
        }

        // POST: api/Groups
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<int>> Post(string[] arrT)
        {
            var cours = await _context.Courses.FindAsync(Int32.Parse(arrT[1]));
            var teacher = await _context.Teachers.FindAsync(Int32.Parse(arrT[2]));

            Group @group = new Group { name = arrT[0], id_cours = cours.id, id_teacher = teacher.Id };

            _context.Groups.Add(@group);
            await _context.SaveChangesAsync();

            return  @group.id ;
        }
        


        public async Task<List<Group>> SortGroup(IQueryable<Group> h, string sort, bool reverse)
        {
            if (sort == "id_cours")
            {
                if (reverse)
                    return  await h.OrderByDescending(s => s.id_cours).ToListAsync();
                else
                    return await h.OrderBy(s => s.id_cours).ToListAsync();
            }

            else if (sort == "id_teacher")
            {
                if (reverse)
                    return await h.OrderByDescending(s => s.id_teacher).ToListAsync();
                else
                    return await h.OrderBy(s => s.id_teacher).ToListAsync();
            }
            else if (reverse)
                return await h.OrderByDescending(s => s.id_teacher).ToListAsync();

            else 
                return await h.OrderBy(s => s.name).ToListAsync();
        }

        // GET: api/Groups
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Group>>> PostSort([FromBody] Body b )
        {
          

            IQueryable<Group> h =  _context.Groups;

            
            List<Group> listT = await SortGroup(h, b.sort, b.reverse);

            int n = listT.Count();

            List<String[]> arrT = new List<String[]>();

            for (int i = 0; i < n; i++)
            {
                var cours = await _context.Courses.FindAsync(listT[i].id_cours);
                var teacher = await _context.Teachers.FindAsync(listT[i].id_teacher);
                string cname = "";

                if (cours != null)
                    cname = cours.name;

                String[] item = { listT[i].id.ToString(), listT[i].name, cname, teacher.Name };

                arrT.Add(item);
            }

            return new ObjectResult(arrT);
            // return await _context.Groups.ToListAsync();
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteGroup(int id)
        {
            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();

            return @group.id;
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.id == id);
        }
    }
}
