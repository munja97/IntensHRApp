using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data
{
    public class HRContext:DbContext
    {
        public HRContext()
        {
        }

        public HRContext(DbContextOptions<HRContext> options):base(options)
        {

        }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Skill> Skills { get; set; }

    }
}
