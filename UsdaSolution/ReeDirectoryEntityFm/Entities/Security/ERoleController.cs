using ReeDirectoryEntityFm.Entities.Base;

namespace ReeDirectoryEntityFm.Entities.Security
{
    public class ERoleController : EBase
    {
        public int Add { get; set; }
        public int Edit { get; set; }
        public int Delete { get; set; }
        public int Print { get; set; }

        public int Role_Id { get; set; }        
        public ERole Role { get; set; }
        
        public int Controller_Id { get; set; }        
        public EController Controller { get; set; }
    }
}