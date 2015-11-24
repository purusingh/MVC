using ReeDirectoryEntityFm.Entities.Base;

namespace ReeDirectoryEntityFm.Entities.Security
{
    public class ERoleUser : EBase
    {

        public int Role_Id { get; set; }        
        public ERole Role { get; set; }
        
        public int User_Id { get; set; }
        public EUser User { get; set; }
    }
}