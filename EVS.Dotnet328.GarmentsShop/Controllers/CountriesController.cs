using EVS.Dotnet328.GarmentsShop.Models;
using EVS.Dotnet328.GarmentsShop.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVS.Dotnet328.GarmentsShop.Controllers
{
    public class CountriesController : Controller
    {
        [HttpGet]
        public ActionResult Manage()
        {
            return View(new LocationsHandler().GetCountries().ToModelList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("~/Views/Countries/_CreateView.cshtml");
        }

        [HttpPost]
        public ActionResult Create(CountryModel model)
        {
            try
            {
                new LocationsHandler().AddCountry(model.ToEntity());

                
                TempData.Add("AlertMessage", new AlertModel("The country is added successfully", AlertModel.AlertType.Success));
            }
            catch (Exception ex)
            {
                //some code to execute in case of error
                TempData.Add("AlertMessage", new AlertModel("Failed to add the country.", AlertModel.AlertType.Error));
            }
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            return PartialView("~/Views/Countries/_UpdateView.cshtml",new LocationsHandler().GetCountry(id).ToModel());
        }

        [HttpPost]
        public ActionResult Update(CountryModel model)
        {
            try
            {
                new LocationsHandler().UpdateCountry(model.Id, model.ToEntity());
                TempData.Add("AlertMessage", new AlertModel("Information Updated Successfully.", AlertModel.AlertType.Success));
            }
            catch
            {
                TempData.Add("AlertMessage", new AlertModel("Failed to Update the country.", AlertModel.AlertType.Error));
            }
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return PartialView("~/Views/Countries/_DeleteView.cshtml", new LocationsHandler().GetCountry(id).ToModel());
        }

        [HttpPost]
        public ActionResult Delete(CountryModel model)
        {
            try
            {
                new LocationsHandler().DeleteCountry(model.Id);
                TempData.Add("AlertMessage",new AlertModel("Country Delete Successfully", AlertModel.AlertType.Success));
            }
            catch
            {
                TempData.Add("AlertMessage", new AlertModel("Failed To Delete Country",AlertModel.AlertType.Error));
            }
            return RedirectToAction("Manage");
        }
    }
}