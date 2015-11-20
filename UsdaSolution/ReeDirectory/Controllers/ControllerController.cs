using System.Web.Mvc;
using System.Linq;
using ReeDirectory.EntityFM.Entities.Security;

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
