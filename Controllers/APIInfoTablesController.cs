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
    [Route("api/T")]
    [ApiController]

   
    public class APIInfoTablesController : ControllerBase
    {
        TableContext db;
        public APIInfoTablesController(TableContext context)
        {
            db = context;
            int c = 6;

            if (db.InfoTables.Count()!=c) //db.SomeEntityCollection.Count()
            {
                

                InfoTable[] T = new InfoTable[c];
                T[0] = new InfoTable { Name = "Teachers", Rus_name = "Преподаватели" };
                T[1] = new InfoTable { Name = "Courses", Rus_name = "Курсы" };
                T[2] = new InfoTable { Name = "Groups", Rus_name = "Группы" };
                T[3] = new InfoTable { Name = "Students", Rus_name = "Ученики" };
                T[4] = new InfoTable { Name = "StudentCourses", Rus_name = "Договоры" };

                for (int i = db.InfoTables.Count(); i < c - 1; i++)
                {
                    db.InfoTables.Add(T[i]);
                }
               
                db.SaveChanges();
            }
        }

        [HttpGet("{name}")]

        public async Task<ActionResult<InfoTable>> Get(string name)
        {
            InfoTable itable = await db.InfoTables.FirstOrDefaultAsync(x => x.Name == name);
            if (itable == null)
                return NotFound();
            return new ObjectResult(itable);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<InfoTable>>> Get()
        {
            
            return await db.InfoTables.ToListAsync();
        }

       
        
    }

    

}