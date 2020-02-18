using EVS.Dotnet328.GarmentsShop.Models;
using EVS.Dotnet328.GarmentsShop.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVS.Dotnet328.GarmentsShop.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Manage()
        {
            ViewBag.Department = new GarmentsHandler().GetDepartments().ToSelectItemList();
            return View(new GarmentsHandler().GetCategoriesList().ToCategoryModel());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Departments = new GarmentsHandler().GetDepartments().ToSelectItemList();
            return PartialView("~/Views/Categories/_CreateView.cshtml");
        }

        [HttpPost]
        public ActionResult Create(CategoryModel modal)
        {
            try
            {
                //some code to add in database
                
                new GarmentsHandler().AddCategory(modal.ToCategoryEntity()); 
                TempData.Add("AlertMessage", new AlertModel("The department is added successfully",AlertModel.AlertType.Success));
            }
            catch(Exception ex)
            {
                //some code to execute in case of error
                TempData.Add("AlertMessage", new AlertModel("Failed to add the department.", AlertModel.AlertType.Error));
            }
            return RedirectToAction("Manage");
        }


        [HttpGet]
        public ActionResult Update(int id)
        {
            //get department from database send it in model form to update view

            ViewBag.Departments = new GarmentsHandler().GetDepartments().ToSelectItemList();

            return PartialView("~/Views/Categories/_UpdateView.cshtml",new GarmentsHandler().GetCategoryByID(id).ToCatModel());
        }

        [HttpPost]
        public ActionResult Update(CategoryModel model)
        {
            try
            {
                //some code to add in database
                new GarmentsHandler().UpdateCategory(model.Id, model.ToCategoryEntity());
                TempData.Add("AlertMessage", new AlertModel("The department is added successfully", AlertModel.AlertType.Success));
            }
            catch (Exception ex)
            {
                //some code to execute in case of error
                TempData.Add("AlertMessage", new AlertModel("Failed to add the department.", AlertModel.AlertType.Error));
            }
            return RedirectToAction("Manage",new GarmentsHandler().GetCategoriesList().ToCategoryModel());
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            //get department from database send it in model form to update view
            ViewBag.Departments = new GarmentsHandler().GetDepartments().ToSelectItemList();
            return PartialView("~/Views/Categories/_DeleteView.cshtml", new GarmentsHandler().GetCategoryByID(id).ToCatModel());
        }

        [HttpPost]
        public ActionResult Delete(CategoryModel model)
        {
            try
            {
                //some code to delete in database
                new GarmentsHandler().DeleteCategoryById(model.Id);
                TempData.Add("AlertMessage", new AlertModel("The department is added successfully", AlertModel.AlertType.Success));
            }
            catch (Exception ex)
            {
                //some code to execute in case of error
                TempData.Add("AlertMessage", new AlertModel("Failed to add the department.", AlertModel.AlertType.Error));
            }
            return RedirectToAction("Manage",new GarmentsHandler().GetCategoriesList().ToCategoryModel());
        }

        [HttpGet]
        public ActionResult GetCategoriesByDept(int id)
        {
            if(id==0)
                return Json(new GarmentsHandler().GetCategoriesList(), JsonRequestBehavior.AllowGet);
            else
                return Json(new GarmentsHandler().GetCategories(new Department { Id=id}),JsonRequestBehavior.AllowGet);
        }

        //get level2 categories based on id of level1
        [HttpGet]
        public ActionResult Level2(int id)
        {
            DDLModel model = new DDLModel();
            model.Caption = "- Select Category -";
            model.Name = "Category";
            model.GlyphIcon = "glyphicon-tags";
            model.Values = new GarmentsHandler().GetCategories(new Department { Id = Convert.ToInt32(id) }).ToSelectItemList();
            return PartialView("~/Views/Shared/_DDListView.cshtml", model);
        }

        //get level3 categories based on id of level1
        [HttpGet]
        public ActionResult Level3(int id)
        {
            DDLModel model = new DDLModel();
            model.Caption = "- Select Sub Category -";
            model.Name = "SubCategory";
            model.GlyphIcon = "glyphicon-tags";
            model.Values = new GarmentsHandler().GetSubCategoriesByCategory(new Category { Id = Convert.ToInt32(id) }).ToSelectItemList();
            return PartialView("~/Views/Shared/_DDListView.cshtml", model);
        }
    }
}