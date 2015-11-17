using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using ReeDirectory.EntityFM.EMaps;
using ReeDirectory.EntityFM.EMaps.Security;
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

            ConfigurationRegistrar cfg = modelBuilder.Configurations;
            cfg.Add(new MCountry());
            cfg.Add(new MState());
            cfg.Add(new MCity()); 
           

            //security
            cfg.Add(new MRole());
            cfg.Add(new MController());
            cfg.Add(new MRoleController());
            cfg.Add(new MUser());
            cfg.Add(new MRoleUser());
        }

        public DbSet<ECountry> Countries { get; set; }
        public DbSet<EState> States { get; set; }
        public DbSet<ECity> Cities { get; set; }
    }
}