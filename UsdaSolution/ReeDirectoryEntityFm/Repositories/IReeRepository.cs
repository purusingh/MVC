using System.Collections.Generic;
using ReeDirectoryEntityFm.Entities.Base;

namespace ReeDirectoryEntityFm.Repositories
{
    public interface IReeRepository<Entity> where Entity : EBase
    {
        IEnumerable<Entity> SelectAll();
        Entity GetByID(object id);
        Entity Insert(Entity obj);
        void Update(Entity obj);
        void Delete(object id);
        //void Save();
    }
}
