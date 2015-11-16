using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ReeDirectory.EntityFM.EMaps;
using ReeDirectory.EntityFM.Entities;

namespace ReeDirectory.EntityFM.Context
{
    public class ReeDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new MCountry());
            modelBuilder.Configurations.Add(new MState());
            modelBuilder.Configurations.Add(new MCity());            
        }

        public DbSet<ECountry> Countries { get; set; }
        public DbSet<EState> States { get; set; }
        public DbSet<ECity> Cities { get; set; }
    }
}