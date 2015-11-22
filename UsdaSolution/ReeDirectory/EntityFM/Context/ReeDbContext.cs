using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using ReeDirectory.EntityFM.EMaps;
using ReeDirectory.EntityFM.EMaps.Security;
using ReeDirectory.EntityFM.Entities;
using ReeDirectory.EntityFM.Entities.Security;

namespace ReeDirectory.EntityFM.Context
{
    public class ReeDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ReeDbContext>(null);
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
            cfg.Add(new MEmployee());
        }

        public DbSet<ECountry> Countries { get; set; }
        public DbSet<EState> States { get; set; }
        public DbSet<ECity> Cities { get; set; }

        public DbSet<EUser> Users { get; set; }
        public DbSet<ERole> Roles { get; set; }
        public DbSet<ERoleUser> RoleUsers { get; set; }

        public DbSet<EController> Controllers { get; set; }
        public DbSet<ERoleController> RoleControllers { get; set; }
        public DbSet<EEmployee> Employees { get; set; }
    }
}