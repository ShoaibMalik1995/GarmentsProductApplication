using EVS.Dotnet328.GarmentsShop.Models;
using EVS.Dotnet328.GarmentsShop.Models.Users;
using EVS.Dotnet328.UsersMgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace EVS.Dotnet328.GarmentsShop.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            string qs = Request.QueryString["rurl"];
            ViewBag.ReturnUrl = qs;

            HttpCookie temp = new HttpCookie(WebUtil.MY_COOKIE);
            if (temp != null)
            {
                temp.Expires = DateTime.Now.AddDays(5);
                Response.SetCookie(temp);
                if (temp.Value != null)
                {
                    string[] loginData = temp.Value.Split(',');
                    model.LoginId = loginData[0];
                    model.Password = loginData[1];
                }
            }

            return View("~/Views/Users/Login.cshtml",model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                User currentUser = new UserHandler().GetUser(model.ToLoginEntity());
                if (currentUser != null)
                {
                    Session.Add(WebUtil.CURRENT_USER, currentUser);
                    if (model.RememberMe)
                    {
                        HttpCookie cookie = new HttpCookie(WebUtil.MY_COOKIE);
                        cookie.Expires = DateTime.Now.AddDays(5);
                        cookie.Value = $"{model.LoginId},{model.Password}";
                        Response.SetCookie(cookie);
                    }

                    //string qs = Request.QueryString["ReturnUrl"];
                    //ViewBag.ReturnRul = qs;
                    //string[] parts = (qs != null) ? qs.Split('/') : null;
                    //if (qs != null && parts.Count() == 2) return RedirectToAction(parts[1], parts[0]);

                    //Check Whether an Admin Role Or User Role
                    if (currentUser.IsInRole(WebUtil.ADMIN_ROLE))
                    {
                        return RedirectToAction("Manage", "Products");
                    }
                    else
                    {
                        return RedirectToAction("Manage", "Products");
                    }
                }

                
            }
            catch (Exception ex)
            {
                TempData.Add("AlertMessage", new AlertModel($"Please Try Correct Email Or Password ! {ex.Message}", AlertModel.AlertType.Error));
            }

            int attempts = Convert.ToInt32(Request["LoginAttempts"]);
            if (++attempts > 2)
            {
                return RedirectToAction("ChangePassword", "UserManagement");
            }
            ViewBag.LoginAttempts = attempts;

            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            ViewBag.Countries = new LocationsHandler().GetCountries().ToSelectItemList();
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(FormCollection collection)
        {
            try
            {
                User u = new User();
                if (!String.IsNullOrWhiteSpace(collection["FirstName"]))
                {
                    u.Name = collection["FirstName"];
                    if (!String.IsNullOrWhiteSpace(collection["LastName"]))
                    {
                        u.Name = u.Name + " " + collection["LastName"];
                    }
                }

                u.ContactNumber = collection["MobileNo"];
                u.BirthDate = Convert.ToDateTime(collection["DOB"]);
                u.Email = collection["Email"];
                int lastindex = collection["Email"].LastIndexOf("@");
                u.LoginId = collection["Email"].Substring(0, lastindex);
                u.Password = collection["Password"];

                u.Address = new Address {
                    StreetAddress = collection["Address"],
                    CityId = Convert.ToInt32(collection["DDLCity"]),
                    City = new LocationsHandler().GetCityByID(Convert.ToInt32(collection["DDLCity"]))
                };
                Role userRole = new UserHandler().GetUserRole("User");
                u.RoleId = userRole.Id;
                u.Role = userRole;
                u.IsActive = true;

                new UserHandler().RegisterUser(u);

                TempData.Add("AlertMessage", new AlertModel("Register Successfully !", AlertModel.AlertType.Success));
            }
            catch (Exception ex)
            {
                TempData.Add("AlertMessage", new AlertModel("Error Occurred !", AlertModel.AlertType.Error));
            }
            return RedirectToAction("SignUp");
        }

        [HttpGet]
        public ActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecoverPassword(LoginModel model)
        {
            bool status = false;
            string email = model.LoginId;
            try
            {
                status = new UserHandler().ForGetPassword(email);
                if (status)
                {
                    TempData.Add("AlertMessage", new AlertModel("The Password Is Sent To your Email Address Kindly Check !", AlertModel.AlertType.Success));
                }
                else
                {
                    TempData.Add("AlertMessage", new AlertModel("Error Occurred", AlertModel.AlertType.Error));
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

    }
}