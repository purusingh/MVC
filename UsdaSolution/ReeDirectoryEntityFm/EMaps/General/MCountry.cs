using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

using ReeDirectoryEntityFm.Entities.General;
using ReeDirectoryEntityFm.EMaps.Base;


namespace ReeDirectoryEntityFm.EMaps.General
{
    public class MCountry : MBase<ECountry>
    {
        public MCountry()
        {
            //Map(m => {
            //    m.MapInheritedProperties();                  
            //});
            Property(p => p.Abbrivation).IsRequired().HasMaxLength(15).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            Property(p => p.Name).IsRequired().HasMaxLength(120);
        }
    }
}