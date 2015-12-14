using System;
using System.Data.SqlClient;
using System.Linq;
using ReeDirectoryEntityFm.Entities.Security;
using ReeDirectory.Models;

namespace ReeDirectory.Controllers
{
    public class UserController : BaseController<User,EUser>
    {
        protected override void PreCreate()
        {
            ViewBag.Roles = db.GetExternalDbSet<ERole>().ToList();
        }

        protected override void PreCreate(EUser model)
        {
            string selectedRoles = Request.Params["SelectedRoles"];
            db.ExecuteSqlCommand("delete from ERoleUser where User_Id = @UserId", new SqlParameter("UserId", model.Id));
            if (selectedRoles != null)
            {
                string[] roleIds = selectedRoles.Split(new char[] { ',' });
                foreach (string roleId in roleIds)
                {
                    model.RoleUsers.Add(new ERoleUser { Role_Id = Convert.ToInt32(roleId)});
                }
            }
        }

        protected override void OnResultExecuted(System.Web.Mvc.ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
    }
}