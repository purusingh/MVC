using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ReeDirectoryEntityFm.Entities.Base;

namespace ReeDirectoryEntityFm.Repositories
{
    public class ReeRepository<Entity> : IReeRepository<Entity> where Entity : EBase
    {
        private DbContext dbContext;

        public ReeRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Entity> SelectAll()
        {
            return dbContext.Set<Entity>().ToList();
        }

        public Entity GetByID(object id)
        {
            return dbContext.Set<Entity>().Find(id);
        }

        public Entity Insert(Entity obj)
        {
           dbContext.Entry<Entity>(obj).State = EntityState.Added;
           dbContext.SaveChanges();
           return obj;
        }

        public void Update(Entity obj)
        {
            dbContext.Entry<Entity>(obj).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void Delete(object id)
        {
            Entity entity = dbContext.Set<Entity>().Find(id);
            dbContext.Entry<Entity>(entity).State = EntityState.Deleted;
            dbContext.SaveChanges(); 
        }

       
    }
}
