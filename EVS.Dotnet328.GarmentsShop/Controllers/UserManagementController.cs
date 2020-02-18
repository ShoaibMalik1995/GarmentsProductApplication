using EVS.Dotnet328.GarmentsShop.Models;
using EVS.Dotnet328.UsersMgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVS.Dotnet328.GarmentsShop.Controllers
{
    public class UserManagementController : Controller
    {
        // GET: UserManagement
        public ActionResult Manage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserProfile()
        {
            ViewBag.Country = new LocationsHandler().GetCountries().ToSelectItemList();

            User u = new UserHandler().CurrentUser();
            ViewBag.User = u;
            return View();
        }

        [HttpPost]
        public ActionResult UserProfile(FormCollection collection)
        {
            try
            {

            }
            catch(Exception ex)
            {

            }
            return View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection fc)
        {
            
            try
            {

            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}