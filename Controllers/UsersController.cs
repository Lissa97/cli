using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Table.Date;
using Table.Models;
using System.Security.Cryptography;
using System.Text;


namespace Table.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
            if (!_context.Useres.Any()) //db.SomeEntityCollection.Count()
            {
                _context.Useres.Add(new User { login = "ozevgendalf", password = getMd5Hash("24242") });
                _context.Useres.Add(new User { login = "jeka", password = getMd5Hash("fgiuhuh") });
                _context.SaveChanges();
            }
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUseres()
        {
            return await _context.Useres.ToListAsync();
        }
        public class UserResolt { 
            public int id { get; set; }
            public string login { get; set; }
            public string token { get; set; }
            public string token2 { get; set; }
        }

       
        // GET: api/Users/5
        [HttpGet("{login}/{password}")]
        public async Task<ActionResult<UserResolt>> GetUser(string login, string password)
        {
            User user = _context.Useres.FirstOrDefault(x => x.login == login && x.password == getMd5Hash(password));

            if (user == null)
            {
                return NotFound();
            }

            Random rnd = new Random();
            string token = rnd.Next().ToString();
            string token2 = rnd.Next().ToString();

            DateTime now = DateTime.Now;
            user.firstToken = token;
            user.dateFirstToken = now.AddMinutes(30);

            user.secondToken = token2;
            user.dateSecondToken = now.AddDays(15);

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }

            UserResolt userR = new UserResolt {id = user.Id, login = login, token = token, token2 = token2 };

            return userR;
        }
        public class P{ 
            public int id { get; set; }
            public string token { get; set; }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<string>> GetName(P res)
        {
            User user = _context.Useres.FirstOrDefault(x => x.Id == res.id);

            if (user == null)
            {
                return "";
            }

            return user.login;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<bool>> GetAccess(P res)
        {
            User user = _context.Useres.FirstOrDefault(x => x.Id == res.id && x.firstToken == res.token);
            DateTime now = DateTime.Now;

            if (user == null)
            {
                return false;
            }

            if (user.dateFirstToken < now)
            {
                return false;
            }
            
            return true;
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<UserResolt>> GetAccessSecondToken(P res)
        {
            User user = _context.Useres.FirstOrDefault(x => x.Id == res.id && x.secondToken == res.token);
            DateTime now = DateTime.Now;

            if (user == null)
            {
                return NotFound();
            }

            Random rnd = new Random();
            string token = rnd.Next().ToString();

            user.firstToken = token;
            user.dateFirstToken = now.AddMinutes(30);

            UserResolt userR = new UserResolt { id = user.Id, login = user.login, token = token, token2 = res.token };

            if (user.dateSecondToken < now)
            {
                string token2 = rnd.Next().ToString();

                user.secondToken = token2;
                user.dateSecondToken = now.AddDays(15);

                userR = new UserResolt {id = user.Id, login = user.login,  token = token, token2 = token2 };
                
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return userR;
        }
        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Useres.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Useres.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Useres.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Useres.Any(e => e.Id == id);
        }

        static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
