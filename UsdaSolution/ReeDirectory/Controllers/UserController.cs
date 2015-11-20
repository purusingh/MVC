using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReeDirectory.EntityFM.Entities.Security;
using ReeDirectory.Models;

namespace ReeDirectory.Controllers
{
    public class UserController : BaseController<User,EUser>
    {
        protected override void PreCreate()
        {
            ViewBag.Roles = db.Roles.ToList();
        }

        protected override void PreCreate(EUser model)
        {
            string selectedRoles = Request.Params["SelectedRoles"];
            if (selectedRoles != null)
            {
                string[] roleIds = selectedRoles.Split(new char[] { ',' });
                foreach (string roleId in roleIds)
                {
                    model.RoleUsers.Add(new ERoleUser { Role_Id = Convert.ToInt32(roleId)});
                }
            }
        }
    }
}