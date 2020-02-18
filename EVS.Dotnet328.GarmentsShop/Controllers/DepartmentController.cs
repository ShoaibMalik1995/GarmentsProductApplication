using EVS.Dotnet328.GarmentsShop.Models;
using EVS.Dotnet328.GarmentsShop.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVS.Dotnet328.GarmentsShop.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Manage()
        {
            return View(new GarmentsHandler().GetDepartments().ToDepartmentModelList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("~/Views/Department/_Create.cshtml");
        }

        [HttpPost]
        public ActionResult Create(DepartmentModel model)
        {
            try
            {
                new GarmentsHandler().AddDepartment(model.ToDepartmentEntity());
                TempData.Add("AlertMessage", new AlertModel("Department Added Successfully", AlertModel.AlertType.Success));
            }
            catch (Exception ex)
            {
                TempData.Add("AlertMessage", new AlertModel("Failed To Add Department", AlertModel.AlertType.Error));
            }
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return PartialView("~/Views/Department/_Edit.cshtml", new GarmentsHandler().GetDepartment(id).ToDepartmentModel());
        }

        [HttpPost]
        public ActionResult Edit(DepartmentModel model)
        {
            try
            {
                new GarmentsHandler().UpdateDepartment(model.Id,model.ToDepartmentEntity());
                TempData.Add("AlertMessage", new AlertModel("Department Updated Successfully", AlertModel.AlertType.Success));
            }
            catch (Exception ex)
            {
                TempData.Add("AlertMessage", new AlertModel("Failed To Update Department", AlertModel.AlertType.Error));
            }
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("~/Views/Department/_Delete.cshtml", new GarmentsHandler().GetDepartment(id).ToDepartmentModel());
        }

        [HttpPost]
        public ActionResult Delete(DepartmentModel model)
        {
            try
            {
                new GarmentsHandler().DeleteDepartment(model.ToDepartmentEntity());
                TempData.Add("AlertMessage", new AlertModel("Department Delete Successfully", AlertModel.AlertType.Success));
            }
            catch (Exception ex)
            {
                TempData.Add("AlertMessage", new AlertModel("Failed To Delete Department", AlertModel.AlertType.Error));
            }
            return RedirectToAction("Manage");
        }
    }
}