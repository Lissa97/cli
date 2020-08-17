using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Table.Models
{
    public class User
    {
        public int Id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string firstToken { get; set; }
        public DateTime dateFirstToken { get; set; }
        public string secondToken { get; set; }
        public DateTime dateSecondToken { get; set; }
    }
}
