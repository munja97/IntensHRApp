using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Candidate
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ContactNumber { get; set; }
        public string Email { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
