using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Table.Models;

namespace Table.Date
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
           : base(options)
        {
        }


        public DbSet<User> Useres { get; set; }
        
    }
}
