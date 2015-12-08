using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using ReeDirectoryEntityFm.Contexts;
using ReeDirectoryEntityFm.Entities.Base;

namespace ReeDirectoryEntityFm.Repositories
{
    public class ReeRepository<Entity> : IReeRepository<Entity> where Entity : EBase
    {
        public ReeRepository()
        {
            ReeContext = new ReeDbContext();
        }

        protected DbContext ReeContext { get; set; }

        public DbSet<Entity> ReeDbSet 
        {
            get { return ReeContext.Set<Entity>(); }
        
        }

        public DbSet<Extranal> GetExternalDbSet<Extranal>() where Extranal : EBase
        {
            return ReeContext.Set<Extranal>();
        }

        public List<Entity> SelectAll()
        {
            return ReeContext.Set<Entity>().ToList();
        }

        public List<Entity> SelectAll(List<string> includes)
        {

            IQueryable<Entity> queriable = ReeContext.Set<Entity>();
            foreach (string include in includes)
            {
                queriable = queriable.Include(include);
            }
            return queriable.ToList();
        }

        public Entity GetByID(int id)
        {
            return ReeContext.Set<Entity>().Find(id);
        }

        public List<Entity> GetByIDAsList(int id)
        {
            return ReeContext.Set<Entity>().Where(ent => ent.Id == id).ToList();
        }

        public Entity GetByID(int id, List<string> includes)
        {
            IQueryable<Entity> queriable = ReeContext.Set<Entity>();
            foreach (string include in includes)
            {
                queriable = queriable.Include(include);
            }
            return queriable.FirstOrDefault(e => e.Id == id);
        }

        public Entity Insert(Entity obj)
        {
            ReeContext.Entry<Entity>(obj).State = EntityState.Added;
            ReeContext.SaveChanges();
            return obj;
        }

        public void Update(Entity obj)
        {
            ReeContext.Entry<Entity>(obj).State = EntityState.Modified;
            ReeContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Entity entity = ReeContext.Set<Entity>().Find(id);
            ReeContext.Entry<Entity>(entity).State = EntityState.Deleted;
            ReeContext.SaveChanges(); 
        }

        public List<Entity> SelectAll(List<string> includes, string filterBy, string filterByValue, ref int? totolRecords, string sortByName, string sortByOperation, int NoOfRecord, int currentPage)
        {
            IQueryable<Entity> queriable = ReeContext.Set<Entity>();
            foreach (string include in includes)
            {
                queriable = queriable.Include(include);
            }

            if (!string.IsNullOrEmpty(filterByValue))
                queriable = queriable.Where(string.Format("{0}.ToString().StartsWith(@0)", filterBy), filterByValue);

            if (totolRecords.HasValue)
            totolRecords = queriable.Count();

            if (!string.IsNullOrEmpty(sortByName))
                queriable = queriable.OrderBy(string.Format("{0} {1}", sortByName, sortByOperation));
            
            return  queriable.Skip(currentPage * NoOfRecord - NoOfRecord).Take(NoOfRecord).ToList();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return ReeContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        public List<ExtEntity> SqlQuery<ExtEntity>(string sql, params object[] parameters) where ExtEntity : class
        {
            return ReeContext.Database.SqlQuery<ExtEntity>(sql, parameters).ToList<ExtEntity>();
        }
    }
}
