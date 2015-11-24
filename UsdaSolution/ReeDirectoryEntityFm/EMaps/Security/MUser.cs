using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using ReeDirectoryEntityFm.EMaps.Base;
using ReeDirectoryEntityFm.Entities.Security;

namespace ReeDirectoryEntityFm.EMaps.Security
{
    public class MUser: MBase<EUser>
    {
        public MUser()
        {
            Property(p => p.LoginName).IsRequired().HasMaxLength(120).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            Property(p => p.FirstName).IsRequired().HasMaxLength(120).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));
            Property(p => p.LastName).IsRequired().HasMaxLength(120).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));
            Property(p => p.Email).IsRequired().HasMaxLength(120).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = false }));
            Property(p => p.Phone).IsRequired().HasMaxLength(10);            
        }
    }
}