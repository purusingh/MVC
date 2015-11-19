using System.Web.Mvc;
using System.Linq;
using ReeDirectory.Models;
using ReeDirectory.EntityFM.Entities;
using ReeDirectory.EntityFM.Context;
using System.Linq.Dynamic;
using System.Data.Entity;
using ReeDirectory.ActionFilters;
using ReeDirectory.EntityFM.Entities.Security;
using System.Collections.Generic;
using ReeDirectory.EntityFM.ExternalEntity;
using System.Data.SqlClient;

namespace ReeDirectory.Controllers
{
    public abstract class BaseController<T, E> : Controller
        where E : EBase, new()
        where T : Base<E>, new()
    {
        #region fields
        protected ReeDbContext db;
        #endregion fileds

        #region Constructors
        public BaseController()
        {
            db = new ReeDbContext();
        }
        public BaseController(ReeDbContext db)
        {
            this.db = db;            
        }

        #endregion Constructors

        #region Security
        private ESecurity security;
        protected ESecurity Security
        {
            get
            {
                if (security == null)
                    security = db.Database.SqlQuery<ESecurity>("[dbo].[PrPermission] @controllerName, @login", new SqlParameter("controllerName", this.GetType().Name), new SqlParameter("login", HttpContext.User.Identity.Name)).FirstOrDefault();
                return security;
            }
        }
        #endregion security

        #region ActionMethods
        [ReeAuthorizeAttribute]
        public ActionResult Index()
        {
            ESecurity eSecurity = Security;
            T model = new T();
            model.CurrentPage = 1;
            model.SortByName = "Id";
            model.SortByOperation = "Desc";
            return Sort(model);
        }


        protected virtual void PreCreate()
        {
        }

        [HttpPost]
        public ActionResult Sort(T model)
        {
            IQueryable<E> queriable = db.Set<E>();

            if (!string.IsNullOrEmpty(model.FilterByValue))
                queriable = queriable.Where(string.Format("{0}.Contains(@0)", model.FilterBy), model.FilterByValue);

            model.TotolRecords = queriable.Count();

            if (!string.IsNullOrEmpty(model.SortByName))
                queriable = queriable.OrderBy(string.Format("{0} {1}", model.SortByName, model.SortByOperation));

            if (model.CurrentPage < 1) //When there is no record
                model.CurrentPage = 1;

            model.Entities = queriable.Skip(model.CurrentPage * model.NoOfRecord - model.NoOfRecord).Take(model.NoOfRecord).ToList();
            ViewBag.Security = Security;
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            PreCreate();
            return View();
        }

        protected virtual void PreCreate(E model)
        { 
        }

        [HttpPost]
        public ActionResult Create(E model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry<E>(model).State = EntityState.Added;
                    PreCreate(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("Duplicate", "Duplicate entry.");
                }
            }
            PreCreate();
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int iD)
        {
            try
            {
                E entity = db.Set<E>().FirstOrDefault(e => e.Id == iD);
                db.Set<E>().Remove(entity);
                db.SaveChanges();
            }
            catch
            { 
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int iD, FormCollection collection)
        {
            try
            {
                E entity = db.Set<E>().FirstOrDefault(ent => ent.Id == iD);
                db.Entry<E>(entity).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("someThingWentWrong", "Error");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int iD)
        {
            try
            {
                E entity = db.Set<E>().FirstOrDefault(e => e.Id == iD);
                return View(entity);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(int iD, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    E entity = new E();
                    UpdateModel(entity);
                    db.Entry<E>(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("Error", "Something went wrong.");
                }
            }

            return View();
        }
        #endregion Actionmethods
    }
}