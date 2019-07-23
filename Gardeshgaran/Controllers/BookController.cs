using Repository;
using Repository.Models.ViewModels;
using System;
using System.Data.Entity.Core.Objects;
using System.Web.Mvc;

namespace Gardeshgaran.Controllers
{
    public class BookController : Controller
    {
        public ActionResult GetFlightInfo(int FlightID)
        {
            return View();
        }
        public ActionResult AddPassanger()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPassanger(AddPassangerViewModel ViewModel)
        {
            return View();
        }
    }
}