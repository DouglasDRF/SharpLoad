using Microsoft.EntityFrameworkCore;
using SharpLoad.Core.Models;
using SharpLoad.Infra.Data.Mapping;

namespace SharpLoad.Infra.Data.Contexts
{
    public class MainContext : DbContext
    {
        public DbSet<LoadTestScript> LoadTestScripts { get; set; }
        public MainContext() { }

        public MainContext(DbContextOptions options) : base(options) 
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=SharpLoad.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LoadTestScriptMapping());
            modelBuilder.ApplyConfiguration(new RequestMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
       
}
