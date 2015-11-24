using System.Web.Mvc;
using System.Linq;
using ReeDirectoryEntityFm.Entities.Security;

namespace ReeDirectory.Controllers
{
    public class ControllerController : BaseController<ReeDirectory.Models.Controller, EController>
    {        
        public ActionResult Menu()
        {
            return View(db.Controllers.ToList());
        }
    }
}
