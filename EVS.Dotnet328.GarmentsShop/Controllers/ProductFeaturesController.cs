using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVS.Dotnet328.GarmentsShop.Controllers
{
    public class ProductFeaturesController : Controller
    {
        // GET: ProductFeatures
        public ActionResult Manage()
        {
            ViewBag.Fabrics = new GarmentsHandler().GetFabrics();
            ViewBag.Colors = new GarmentsHandler().GetColors();
            ViewBag.Sizes = new GarmentsHandler().GetSizes();
            return View();
        }

        [HttpPost]
        public ActionResult AddColor()
        {
            try
            {
                //new GarmentsHandler().addColor();
            }
            catch(Exception ex)
            {

            }

            return Json(new GarmentsHandler().GetColors(),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddSize(int number, string name)
        {
            try
            {
                new GarmentsHandler().addSize(new Size { Name=name, Number=number});
            }
            catch (Exception ex)
            {

            }

            return Json(new GarmentsHandler().GetSizes(),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddFabric()
        {
            try
            {
                //new GarmentsHandler().addFabric();
            }
            catch (Exception ex)
            {

            }

            return Json(new GarmentsHandler().GetFabrics(), JsonRequestBehavior.AllowGet);
        }
    }
}