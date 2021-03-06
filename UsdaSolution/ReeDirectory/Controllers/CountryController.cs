﻿using System.Web.Mvc;
using ReeDirectoryEntityFm.Contexts;
using ReeDirectoryEntityFm.Entities.General;
using ReeDirectory.Models;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ReeDirectory.ActionFilters;

namespace ReeDirectory.Controllers
{
    public class CountryController : BaseController<Country,ECountry>
    {
        [AjaxActionFilterAttribute]
        public ActionResult Lists()
        {
            ReeDbContext db = new ReeDbContext();
            IEnumerable<ECountry> countries = db.Countries.OrderBy(ent => ent.Name).ToList();
            if (HttpContext.Request.IsAjaxRequest())
                return Json(new SelectList(countries, "Id", "Name"), JsonRequestBehavior.AllowGet);
            return View(countries);
            
        }

    }
}