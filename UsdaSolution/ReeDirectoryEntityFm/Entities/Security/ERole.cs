using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using ReeDirectoryEntityFm.Entities.Base;
using ReeDirectoryEntityFm.Entities.Employee;



namespace ReeDirectoryEntityFm.Entities.Security
{
    public class ERole : EBase
    {
        public ERole()
        {
            RoleControllers = new List<ERoleController>();
        }
        [Required(ErrorMessage = "Name name is required.")]
        public string Name { get; set; }

        public List<ERoleController> RoleControllers { get; set; }

        public List<ERoleUser> RoleUsers { get; set; }
    }
}