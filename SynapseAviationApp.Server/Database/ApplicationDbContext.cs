using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SynapseAviationApp.Server.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SynapseAviationApp.Server.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
