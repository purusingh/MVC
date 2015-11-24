using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using ReeDirectoryEntityFm.Entities.Base;

namespace ReeDirectoryEntityFm.Entities.Security
{
    public class EUser : EBase
    {
        public EUser()
        {
            RoleUsers = new List<ERoleUser>();
        }
        [Required(ErrorMessage="Login name is required.")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        public string MidName { get; set; }

        [Required(ErrorMessage = "Login name is required.")]
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        public List<ERoleUser> RoleUsers { get; set; }
    }
}