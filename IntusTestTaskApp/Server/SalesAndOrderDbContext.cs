using IntusTestTaskApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntusTestTaskApp.Server
{
    public class SalesAndOrderDbContext : DbContext
    {
        public SalesAndOrderDbContext(DbContextOptions<SalesAndOrderDbContext> options) : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Window> Windows { get; set; }
        public DbSet<SubElement> SubElements { get; set; }
    }
}
