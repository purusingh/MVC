using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using ReeDirectory.EntityFM.Entities.Security;

namespace ReeDirectory.EntityFM.EMaps.Security
{
    public class MRoleController: MBase<ERoleController>
    {
        
        public MRoleController()
        {            
            
            HasRequired(r => r.Role).WithMany(n=>n.RoleControllers).HasForeignKey(k => k.Role_Id);
            HasRequired(r => r.Controller).WithMany(n=>n.RoleControllers).HasForeignKey(k => k.Controller_Id);            

            Property(p => p.Role_Id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute(typeof(ERoleController).Name, 1) { IsUnique = true }));
            Property(p => p.Controller_Id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute(typeof(ERoleController).Name, 2) { IsUnique = true }));
        }
    }
}