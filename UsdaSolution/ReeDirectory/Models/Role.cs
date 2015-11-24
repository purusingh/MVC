using System.Collections.Generic;
using ReeDirectoryEntityFm.Entities.Security;

namespace ReeDirectory.Models
{
    public class Role : Base<ERole>
    {

        public override System.Collections.Generic.List<string> Includes()
        {
            List<string> strings = base.Includes();
            strings.Add("RoleControllers");
            return strings;
        }

    }
}