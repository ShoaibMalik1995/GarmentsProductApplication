using EVS.Dotnet328.GarmentsShop.Models;
using EVS.Dotnet328.GarmentsShop.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVS.Dotnet328.GarmentsShop.Controllers
{
    public class SubCategoriesController : Controller
    {
        // GET: SubCategories
        public ActionResult Manage()
        {
            ViewBag.Department = new GarmentsHandler().GetDepartments().ToSelectItemList();
            return View(new GarmentsHandler().GetSubCategories().ToSubCategoryModelList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Departments = new GarmentsHandler().GetDepartments().ToSelectItemList();
            //ViewBag.Categories = new GarmentsHandler().GetCategoriesList().ToSelectItemList();
            return PartialView("~/Views/SubCategories/_Create.cshtml");
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            try
            {
                SubCategoryModel model = new SubCategoryModel { Name = formCollection["Name"], CategoryId = Convert.ToInt32(formCollection["Category"]) };
                
                new GarmentsHandler().AddSubCategory(model.ToSubCategoryEntity());
                TempData.Add("AlertMessage", new AlertModel("Sub Category is added successfully", AlertModel.AlertType.Success));
            }
            catch (Exception ex)
            {
                
                TempData.Add("AlertMessage", new AlertModel("Failed to add the Sub Category.", AlertModel.AlertType.Error));
            }
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = new GarmentsHandler().GetCategoriesList().ToSelectItemList();
            return PartialView("~/Views/SubCategories/_Edit.cshtml",new GarmentsHandler().GetSubCategoryByID(id).ToSubCategoryModel());
        }

        [HttpPost]
        public ActionResult Edit(SubCategoryModel model)
        {
            try
            {
                new GarmentsHandler().UpdateSubCategory(model.ToSubCategoryEntity());
                TempData.Add("AlertMessage", new AlertModel("Sub Category is Updated successfully", AlertModel.AlertType.Success));
            }
            catch (Exception ex)
            {
                //some code to execute in case of error
                TempData.Add("AlertMessage", new AlertModel("Failed to Update the Sub Category.", AlertModel.AlertType.Error));
            }
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            ViewBag.Categories = new GarmentsHandler().GetCategoriesList().ToSelectItemList();
            return PartialView("~/Views/SubCategories/_Delete.cshtml", new GarmentsHandler().GetSubCategoryByID(id).ToSubCategoryModel());
        }

        [HttpPost]
        public ActionResult Delete(CategoryModel model)
        {
            try
            {
                //some code to delete in database
                new GarmentsHandler().DeleteSubCategoryById(model.Id);
                TempData.Add("AlertMessage", new AlertModel("The department is added successfully", AlertModel.AlertType.Success));
            }
            catch (Exception ex)
            {
                //some code to execute in case of error
                TempData.Add("AlertMessage", new AlertModel("Failed to add the department.", AlertModel.AlertType.Error));
            }
            return RedirectToAction("Manage", new GarmentsHandler().GetCategoriesList().ToCategoryModel());
        }

        [HttpGet]
        public ActionResult Level3(int id)
        {
            return Json(new GarmentsHandler().GetSubCategoriesByCategory(new Category { Id = Convert.ToInt32(id) }),JsonRequestBehavior.AllowGet);
        }
    }
}