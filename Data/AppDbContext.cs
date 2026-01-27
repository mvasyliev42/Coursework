using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Coursework.Models;

namespace Coursework.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Flowers> Flowers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
