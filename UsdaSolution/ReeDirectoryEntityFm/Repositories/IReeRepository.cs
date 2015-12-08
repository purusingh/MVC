using System;
using System.Collections.Generic;
using System.Data.Entity;
using ReeDirectoryEntityFm.Entities.Base;

namespace ReeDirectoryEntityFm.Repositories
{
    public interface IReeRepository<Entity> where Entity : EBase
    {
        DbSet<Entity> ReeDbSet { get; }
        DbSet<Extranal> GetExternalDbSet<Extranal>() where Extranal : EBase;
        List<Entity> SelectAll();
        List<Entity> SelectAll(List<string> includes);
        List<Entity> SelectAll(List<string> includes, string filterBy, string filterByValue, ref int? totolRecords, string sortByName, string sortByOperation, int NoOfRecord, int currentPage);
        Entity GetByID(int id);
        List<Entity> GetByIDAsList(int id);
        Entity GetByID(int id, List<string> includes);
        Entity Insert(Entity obj);
        void Update(Entity obj);
        void Delete(int id);     
        int ExecuteSqlCommand(string sql, params object[] parameters);
        List<ExtEntity> SqlQuery<ExtEntity>(string sql, params object[] parameters) where ExtEntity : class;
        
        //void Save();
    }
}
