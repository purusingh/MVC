using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

using ReeDirectoryEntityFm.EMaps.Base;
using ReeDirectoryEntityFm.Entities.Security;

namespace ReeDirectoryEntityFm.EMaps.Security
{
    public class MRoleUser:MBase<ERoleUser>
    {
        public MRoleUser()
        {
            HasRequired(r => r.Role).WithMany(n => n.RoleUsers).HasForeignKey(k => k.Role_Id);
            HasRequired(r => r.User).WithMany(n => n.RoleUsers).HasForeignKey(k => k.User_Id);

            Property(p => p.Role_Id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute(typeof(ERoleUser).Name,1)));
            Property(p => p.User_Id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute(typeof(ERoleUser).Name,2)));
        }
    }
}