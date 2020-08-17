using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Table.Date;
using Table.Models;

namespace Table.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RowTypesController : ControllerBase
    {
        private readonly TableContext _context;

        public RowTypesController(TableContext context)
        {
            _context = context;
            int c = 11;
            //context.RowTypes.RemoveRange(context.RowTypes);
           // context.SaveChanges();
            if (_context.RowTypes.Count() != c) //db.SomeEntityCollection.Count()
            {
              

                RowType[] T = new RowType[c];

                T[0] = new RowType { Table_id = 21, Name = "name", Rus_name = "Имя", Type ="string" };
                T[1] = new RowType { Table_id = 21, Name = "description", Rus_name = "Описание", Type = "text" };
                T[2] = new RowType { Table_id = 22, Name = "name", Rus_name = "Имя", Type = "string" };
                T[3] = new RowType { Table_id = 22, Name = "count_in_week", Rus_name = "Уроков в неделю", Type = "int" };
                T[4] = new RowType { Table_id = 23, Name = "name", Rus_name = "Имя", Type = "string" };
                T[5] = new RowType { Table_id = 23, Name = "cours", Rus_name = "Курс", Type = "Courses" };
                T[6] = new RowType { Table_id = 23, Name = "teacher", Rus_name = "Преподаватель", Type = "Teachers" };
                T[7] = new RowType { Table_id = 24, Name = "name", Rus_name = "Имя", Type = "string" };
                T[8] = new RowType { Table_id = 25, Name = "dogovor", Rus_name = "Номер договора", Type = "int" };
                T[9] = new RowType { Table_id = 25, Name = "student", Rus_name = "Студент", Type = "Students" };
                T[10] = new RowType { Table_id = 25, Name = "course", Rus_name = "Курс", Type = "Courses" };

                for (int i = _context.RowTypes.Count(); i < c; i++)
                {
                    _context.RowTypes.Add(T[i]);
                }
               
                _context.SaveChanges();
            }

            
        }

        // GET: api/RowTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RowType>>> GetRowTypes()
        {
            return await _context.RowTypes.ToListAsync();
        }

        // GET: api/RowTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<RowType>>> GetRowType(int id)
        {
            var rowType = await _context.RowTypes.Where(x => x.Table_id == id).ToListAsync();

            if (rowType.Count() < 0)
            {
                return NotFound();
            }

            return rowType;
        }

        // PUT: api/RowTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRowType(int id, RowType rowType)
        {
            if (id != rowType.id)
            {
                return BadRequest();
            }

            _context.Entry(rowType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RowTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RowTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RowType>> PostRowType(RowType rowType)
        {
            _context.RowTypes.Add(rowType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRowType", new { id = rowType.id }, rowType);
        }

        // DELETE: api/RowTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RowType>> DeleteRowType(int id)
        {
            var rowType = await _context.RowTypes.FindAsync(id);
            if (rowType == null)
            {
                return NotFound();
            }

            _context.RowTypes.Remove(rowType);
            await _context.SaveChangesAsync();

            return rowType;
        }

        private bool RowTypeExists(int id)
        {
            return _context.RowTypes.Any(e => e.id == id);
        }
    }
}
