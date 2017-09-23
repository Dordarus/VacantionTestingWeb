using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static VacantionTestingWeb.Models.Parser;
using VacantionTestingWeb.Models;
using System.Web.Mvc;

namespace VacantionTestingWeb.Controllers
{
    public class HomeController : Controller
    {
        private static Group group;

        public ActionResult Index()
        {
            group = Parse("http://91.232.241.66:92/api/v1/study/89b4effb-40f8-44d9-ba25-c9cbe7602a95/measurements/tasks/3ba2733b-6274-4bee-93ac-6af4eab90e72/?authorization=Basic%20dGVzdF92YWNhbnRpb246dGVzdF92YWNhbnRpb24=");
            ViewBag.Title = group.Name;
            return View();
        }

        [HttpGet]
        public JsonResult GetJSON()
        {
            List<ChartViewModel> cvw = new List<ChartViewModel>();
           
            foreach (var item in group.Sessions)
            {
                cvw.Add(new ChartViewModel { Date = item.Date.ToShortDateString(), AvgValue = item.AvgValue });
            }

            return Json(new { Sessions = cvw }, JsonRequestBehavior.AllowGet);
        }
    }
}