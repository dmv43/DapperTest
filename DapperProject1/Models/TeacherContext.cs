using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DapperProject1.Models
{
    public class TeacherContext:DbContext
    {
       public DbSet<Teacher> Teachers { get; set; }
        public TeacherContext(DbContextOptions<TeacherContext> options)
            : base(options)
        {
        }
    }
}
