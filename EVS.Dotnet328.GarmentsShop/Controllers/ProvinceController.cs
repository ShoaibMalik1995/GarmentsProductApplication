using EVS.Dotnet328.GarmentsShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVS.Dotnet328.GarmentsShop.Controllers
{
    public class ProvinceController : Controller
    {
        // GET: Province
        public ActionResult Manage()
        {
            ViewBag.Country = new LocationsHandler().GetCountries().ToSelectItemList();
            return View(new LocationsHandler().GetProvince().ToProvinceModelList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Country = new LocationsHandler().GetCountries().ToSelectItemList();
            return PartialView("~/Views/Province/_Create.cshtml");
        }

        [HttpPost]
        public ActionResult Create(ProvinceModal provinceModal)
        {
            try
            {
                new LocationsHandler().AddProvince(provinceModal.ToProvinceEntity());
                TempData.Add("AlertMessage", new AlertModel("Province Added Successfully", AlertModel.AlertType.Success));
            }
            catch(Exception e)
            {
                TempData.Add("AlertMessage", new AlertModel("Failed To Add Province", AlertModel.AlertType.Error));
            }
            
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult Edit( int id)
        {
            ViewBag.Country = new LocationsHandler().GetCountries().ToSelectItemList();
            return PartialView("~/Views/Province/_Edit.cshtml",new LocationsHandler().GetProvinceById(id).ToProvinceModel());
        }

        [HttpPost]
        public ActionResult Edit(ProvinceModal provinceModal)
        {
            try
            {
                new LocationsHandler().UpdateProvince(provinceModal.Id,provinceModal.ToProvinceEntity());
                TempData.Add("AlertMessage", new AlertModel("Province Updated Successfully", AlertModel.AlertType.Success));
            }
            catch (Exception e)
            {
                TempData.Add("AlertMessage", new AlertModel("Failed To Update Province", AlertModel.AlertType.Error));
            }

            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            ViewBag.Country = new LocationsHandler().GetCountries().ToSelectItemList();
            return PartialView("~/Views/Province/_Delete.cshtml", new LocationsHandler().GetProvinceById(id).ToProvinceModel());
        }

        [HttpPost]
        public ActionResult Delete(ProvinceModal provinceModal)
        {
            try
            {
                new LocationsHandler().DeleteProvinceByID(provinceModal.Id);
                TempData.Add("AlertMessage", new AlertModel("Province Deleteed Successfully", AlertModel.AlertType.Success));
            }
            catch (Exception e)
            {
                TempData.Add("AlertMessage", new AlertModel("Failed To Delete Province", AlertModel.AlertType.Error));
            }

            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult GetProvinceByCountry(int id)
        {
            return Json(new LocationsHandler().GetProvinceBycountry(id),JsonRequestBehavior.AllowGet);
        }
    }
}