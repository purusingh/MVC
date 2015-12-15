using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;


using ReeDirectoryEntityFm.EMaps.General;
using ReeDirectoryEntityFm.EMaps.Security;
using ReeDirectoryEntityFm.EMaps.Employee;


using ReeDirectoryEntityFm.Entities.General;
using ReeDirectoryEntityFm.Entities.Security;
using ReeDirectoryEntityFm.Entities.Employee;
using ReeDirectoryEntityFm.EMaps.Surveys;
using ReeDirectoryEntityFm.Entities.Surveys;



namespace ReeDirectoryEntityFm.Contexts
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

            //Surver
            cfg.Add(new MSurvey());
            cfg.Add(new MAnswer());
            cfg.Add(new MOrganization());
            cfg.Add(new MSection());
            cfg.Add(new MQuestion());
            cfg.Add(new MQuestionType());

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

        public DbSet<EOrganization> Organizations { get; set; }
        public DbSet<ESurvey> Surveys { get; set; }
        public DbSet<EQuestion> Questions { get; set; }
        public DbSet<EQuestionType> QuestionTypes { get; set; }
        public DbSet<EAnswer> Answers { get; set; }

    }
}