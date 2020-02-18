using EVS.Dotnet328.GarmentsShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVS.Dotnet328.GarmentsShop.Controllers
{
    public class CityController : Controller
    {
        // GET: City
        public ActionResult Manage()
        {
            ViewBag.Country = new LocationsHandler().GetCountries().ToSelectItemList();
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Country = new LocationsHandler().GetCountries().ToSelectItemList();
            return PartialView("/Views/City/_Create.cshtml");
        }

        [HttpPost]
        public ActionResult Create(FormCollection fc)
        {
            try
            {
                CityModel model = new CityModel { Name = fc["Name"], Province_Id = Convert.ToInt32(fc["DDLProvince"]) };
                new LocationsHandler().AddCity(model.ToCityEntity());
                TempData.Add("AlertMessage", new AlertModel("City is Added Successfully", AlertModel.AlertType.Success));
            }
            catch(Exception ex)
            {
                TempData.Add("AlertMessage", new AlertModel("Failed To Add City", AlertModel.AlertType.Error));
            }

            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Province = new LocationsHandler().GetProvince().ToSelectItemList();
            return PartialView("/Views/City/_Edit.cshtml",new LocationsHandler().GetCityByID(id).ToCityModel());
        }

        [HttpPost]
        public ActionResult Edit(CityModel cityModel)
        {
            try
            {
                new LocationsHandler().UpdateCity(cityModel.Id, cityModel.ToCityEntity());
                TempData.Add("AlertMessage", new AlertModel("Update City Successfully", AlertModel.AlertType.Success));
            }
            catch(Exception ex)
            {
                TempData.Add("AlertMessage", new AlertModel("Failed To Update City", AlertModel.AlertType.Error));
            }

            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            ViewBag.Province = new LocationsHandler().GetProvince().ToSelectItemList();
            return PartialView("/Views/City/_Delete.cshtml", new LocationsHandler().GetCityByID(id).ToCityModel());
        }

        [HttpPost]
        public ActionResult Delete(CityModel cityModel)
        {
            try
            {
                new LocationsHandler().DeleteCity(cityModel.Id);
                TempData.Add("AlertMessage", new AlertModel("Delete City Successfully", AlertModel.AlertType.Success));
            }
            catch (Exception ex)
            {
                TempData.Add("AlertMessage", new AlertModel("Failed To Delete City", AlertModel.AlertType.Error));
            }

            return RedirectToAction("Manage");
        }


        [HttpGet]
        public ActionResult GetProvinceByCountry(int id)
        {
            DDLModel model = new DDLModel();
            model.Name = "DDLProvince";
            model.Caption = "- Select Province -";
            model.GlyphIcon = "glyphicon glyphicon-tags";
            model.Values = new LocationsHandler().GetProvinceBycountry(id).ToSelectItemList();
            return PartialView("~/Views/Shared/_DDListView.cshtml", model);
        }

        [HttpGet]
        public ActionResult CityByProvince(int id)
        {
            DDLModel model = new DDLModel();
            model.Name = "DDLCity";
            model.Caption = "- Select City -";
            model.GlyphIcon = "glyphicon glyphicon-tags";
            model.Values = new LocationsHandler().GetCitiesByProvinceId(id).ToSelectItemList();
            return PartialView("~/Views/Shared/_DDListView.cshtml", model);
        }

        [HttpGet]
        public ActionResult GetCitiesByProvince(int id)
        {
            return Json(new LocationsHandler().GetCitiesByProvinceId(id), JsonRequestBehavior.AllowGet);
        }
    }
}