using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using ReeDirectory.EntityFM.Entities.Security;

namespace ReeDirectory.EntityFM.EMaps.Security
{
    public class MRole: MBase<ERole>
    {
        public MRole()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(120).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
        }
    }
}