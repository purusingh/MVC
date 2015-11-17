
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ReeDirectory.EntityFM.Entities.Security
{
    public class ERole : EBase
    {
        [Required(ErrorMessage = "Name name is required.")]
        public string Name { get; set; }

        public List<ERoleController> RoleControllers { get; set; }

        public List<ERoleUser> RoleUsers { get; set; }
    }
}