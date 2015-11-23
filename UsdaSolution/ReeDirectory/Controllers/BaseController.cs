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
using System.Data.Entity.Validation;

namespace ReeDirectory.Controllers
{
    public abstract class BaseController<T, E> : System.Web.Mvc.Controller
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
                //if (security == null)
                    //security = new ESecurity { Add = 0, Edit = 0, Delete = 0, Print = 0 };
                return security;
            }
        }
        #endregion security


        #region VirtualMethods
        protected virtual void PreCreate()
        {
        }

        protected virtual void PreCreate(E model)
        {
        }
        #endregion Virtualmethods

        #region ActionMethods
        [ReeAuthorizeAttribute]
        public ActionResult Index()
        {
            T model = new T();
            model.CurrentPage = 1;
            model.SortByName = "Id";
            model.SortByOperation = "Desc";
            return Index(model);
        }        

        [HttpPost]
        public ActionResult Index(T model)
        {
            IQueryable<E> queriable = db.Set<E>();
            foreach (string include in model.Includes())
            {
                queriable = queriable.Include(include);
            }

            if (!string.IsNullOrEmpty(model.FilterByValue))
                queriable = queriable.Where(string.Format("{0}.ToString().StartsWith(@0)", model.FilterBy), model.FilterByValue);

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
            if (HttpContext.Request.IsAjaxRequest())
                return PartialView("user/__Create");
            return View();
        }        

        [HttpPost]
        [EncryptedActionAttribute]
        public ActionResult Create(E model)
        {            
            try
            {
                PreCreate(model);
                db.Entry<E>(model).State = EntityState.Added;
                db.SaveChanges();
                if (HttpContext.Request.IsAjaxRequest())
                {
                    ModelState.AddModelError("saved", "Success");
                    return PartialView("user/__Create");
                }
                return RedirectToAction("");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult result in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError error in result.ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
            }
            PreCreate();
            return View();
        }

        [HttpGet]
        [EncryptedActionAttribute]
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
        [EncryptedActionAttribute]
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
        [EncryptedActionAttribute]
        public ActionResult Edit(int iD)
        {
            try
            {
                PreCreate();
                IQueryable<E> queriable = db.Set<E>();
                T model = new T();
                foreach (string include in model.Includes())
                {
                    queriable = queriable.Include(include);
                }
                return View(queriable.FirstOrDefault(e => e.Id == iD));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [EncryptedActionAttribute]
        public ActionResult Edit(int iD, FormCollection collection)
        {
            try
            {
                E entity = new E();
                UpdateModel(entity);
                db.Entry<E>(entity).State = EntityState.Modified;                
                PreCreate(entity);                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult result in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError error in result.ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
            } 
            catch          
            {
            }
            return View();
        }
        #endregion Actionmethods
    }
}