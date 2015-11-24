using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using ReeDirectoryEntityFm.EMaps.Base;
using ReeDirectoryEntityFm.Entities.Security;

namespace ReeDirectoryEntityFm.EMaps.Security
{
    public class MController : MBase<EController>
    {
        public MController()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(120).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            Property(p => p.Path).IsRequired().HasMaxLength(120).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
        }
    }
}