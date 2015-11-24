using System.Collections.Generic;
using ReeDirectoryEntityFm.Entities.Security;

namespace ReeDirectory.Models
{
    public class User: Base<EUser>
    {
        public override System.Collections.Generic.List<string> Includes()
        {
            List<string> strings = base.Includes();
            strings.Add("RoleUsers");
            return strings;
        }
    }
}