
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ReeDirectory.EntityFM.Entities.Security
{
    public class EUser : EBase
    {
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