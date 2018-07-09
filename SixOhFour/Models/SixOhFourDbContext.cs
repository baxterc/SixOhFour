using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SixOhFour.Models
{
    public class SixOhFourDbContext : IdentityDbContext<ApplicationUser>
    {

        public SixOhFourDbContext()
        {

        }

        public virtual DbSet<Event> Comments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Event> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SixOhFourDb;integrated security = True");
        }

        public SixOhFourDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
