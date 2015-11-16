using System.Web.Mvc;
using ReeDirectory.EntityFM.Context;
using ReeDirectory.EntityFM.Entities;
using ReeDirectory.Models;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ReeDirectory.Controllers
{
    public class CountryController : BaseController<Country,ECountry>
    {

        public ActionResult Lists()
        {
            ReeDbContext db = new ReeDbContext();
            IEnumerable<ECountry> countries = db.Countries.OrderBy(ent => ent.Name).ToList();
            //if (HttpContext.Request.IsAjaxRequest())
                return Json(new SelectList(countries, "Id", "Name"), JsonRequestBehavior.AllowGet);
            return View(countries);
            
        }

    }
}