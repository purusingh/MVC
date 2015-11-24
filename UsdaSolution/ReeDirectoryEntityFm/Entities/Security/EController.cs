using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using ReeDirectoryEntityFm.Entities.Base;

namespace ReeDirectoryEntityFm.Entities.Security
{
    public class EController :EBase
    {
        [Required(ErrorMessage = "Name name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Path name is required.")]
        public string Path { get; set; }

        public List<ERoleController> RoleControllers { get; set; }
    }
}