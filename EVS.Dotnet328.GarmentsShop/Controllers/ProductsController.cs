using EVS.Dotnet328.GarmentsShop.Models;
using EVS.Dotnet328.UsersMgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVS.Dotnet328.GarmentsShop.Controllers
{
    public class ProductsController : Controller
    {
        [HttpGet]
        public ActionResult Manage()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && currentUser.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("login", "users", new { rurl = "products/manage" });

            return View(new GarmentsHandler().GetProducts().ToSummaryModelList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && currentUser.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("login", "users", new { rurl = "products/manage" });

            ViewBag.Departments = new GarmentsHandler().GetDepartments().ToSelectItemList();
            ViewBag.Colors = new GarmentsHandler().GetColors().ToSelectItemList();
            ViewBag.Sizes = new GarmentsHandler().GetSizes().ToSelectItemList();
            ViewBag.Fabrics = new GarmentsHandler().GetFabrics().ToSelectItemList();
            
            return View("~/Views/Products/_Create.cshtml");
        }

        [HttpPost]
        public ActionResult Create(FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && currentUser.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("login", "users", new { rurl = "products/manage" });

            try
            {
                Product prod = new Product();
                prod.Name = data["Name"];
                prod.Price = Convert.ToSingle(data["Price"]);
                prod.LaunchingDate = Convert.ToDateTime(data["LaunchingDate"]);
                prod.SubCategory = new SubCategory { Id = Convert.ToInt32(data["SubCategory"]) };

                string[] sizeIdArray = data["SizesOffered"].Split(',');
                foreach (var sizeId in sizeIdArray)
                {
                    prod.SizesOffered.Add(new Size { Id = Convert.ToInt32(sizeId) });
                }
                string[] colorIdArray = data["ColorsOffered"].Split(',');
                foreach (var colorId in colorIdArray)
                {
                    prod.ColorsOffered.Add(new Color { Id = Convert.ToInt32(colorId) });
                }
                prod.Fabric = new Fabric { Id = Convert.ToInt32(data["Fabric"]) };
                prod.Description = data["Description"];
                int counter = 0;
                long uno = DateTime.Now.Ticks;
                foreach (string fcName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fcName];
                    if (!string.IsNullOrWhiteSpace(file.FileName))
                    {
                        string url = $"~/dataimages/products/{uno}_{++counter}{file.FileName.Substring(file.FileName.LastIndexOf('.'))}";
                        string path = Server.MapPath(url);
                        file.SaveAs(path);
                        prod.Images.Add(new ProductImage { Url = url, Priority = counter });
                    }
                }
                new GarmentsHandler().AddProduct(prod);
                TempData.Add("AlertMessage", new AlertModel("The Product is Added Successfully", AlertModel.AlertType.Success));
            }
            catch(Exception ex)
            {
                TempData.Add("AlertMessage", new AlertModel("Failed to add the Product.", AlertModel.AlertType.Error));
            }
            return RedirectToAction("manage");
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            return PartialView("~/Views/Products/_Detail.cshtml",new GarmentsHandler().GetProduct(id).ToSummaryModel());
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && currentUser.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("login", "users", new { rurl = "products/manage" });

            return PartialView("~/Views/Products/_Delete.cshtml", new GarmentsHandler().GetProduct(id).ToSummaryModel());
        }

        [HttpPost]
        public ActionResult Delete(int id,SummaryModel model)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && currentUser.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("login", "users", new { rurl = "products/manage" });

            try
            {
                new GarmentsHandler().DeleteProduct(id);
                TempData.Add("AlertMessage", new AlertModel("The Product is Deleted Successfully", AlertModel.AlertType.Success));
            }
            catch(Exception ex)
            {
                TempData.Add("AlertMessage", new AlertModel("Error Occur", AlertModel.AlertType.Error));
            }
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && currentUser.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("login", "users", new { rurl = "products/manage" });
            
            ViewBag.Departments = new GarmentsHandler().GetDepartments().ToSelectItemList();
            ViewBag.Colors = new GarmentsHandler().GetColors().ToSelectItemList();
            ViewBag.Sizes = new GarmentsHandler().GetSizes().ToSelectItemList();
            ViewBag.Fabrics = new GarmentsHandler().GetFabrics().ToSelectItemList();

            ViewBag.Categories = new GarmentsHandler().GetCategoriesList().ToSelectItemList();
            ViewBag.SubCategories = new GarmentsHandler().GetSubCategories().ToSelectItemList();

            return PartialView("~/Views/Products/_Edit.cshtml", new GarmentsHandler().GetProduct(id).ToSummaryModel());
        }

        [HttpPost]
        public ActionResult Edit(int id,SummaryModel modal)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && currentUser.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("login", "users", new { rurl = "products/manage" });

            try
            {
                TempData.Add("AlertMessage", new AlertModel("The Product is Updated Successfully", AlertModel.AlertType.Success));
            }
            catch(Exception ex)
            {
                TempData.Add("AlertMessage", new AlertModel("Error Occured", AlertModel.AlertType.Error));
            }
            return RedirectToAction("Manage");
        }
    }
}