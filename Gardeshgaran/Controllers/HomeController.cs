using System.Collections.Generic;
using System.Web.Mvc;
using Repository.Models.ViewModels;
using System;
using Application.Services;

namespace Gardeshgaran.Controllers
{
    public class HomeController : BaseController
    {
        FlightService flightService;
        public HomeController()
        {
            flightService = FlightService.GetInstance();
        }

        public ActionResult SearchFlight()
        {
            try
            {
                ServiceResponseModel response = flightService.GetAirportSelectList();
                if (response.Data != null)
                {
                    TempData["Airports"] = response.Data;
                }
                else
                {
                    ViewBag.Error = response.ErrorString;
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("HTTP500", "Error", new { ErrorMessage = e.Message });
            }

            return View(new List<FlightViewModel>());
        }

        [HttpPost]
        public ActionResult SearchFlight(FormCollection searchForm)
        {
            List<FlightViewModel> AllFlights = new List<FlightViewModel>();

            try
            {
                ServiceResponseModel response = flightService.GetAirportSelectList();
                if (response.Data != null)
                {
                    TempData["Airports"] = response.Data;
                }
                else
                {
                    ViewBag.Error = response.ErrorString;
                }
                ServiceResponseModel flightRecponse = flightService.GetAvailablity(searchForm);
                if (flightRecponse.Data != null)
                {
                    AllFlights = (List<FlightViewModel>)flightRecponse.Data;
                }
                else
                {
                    ViewBag.Error = flightRecponse.ErrorString;
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("HTTP500", "Error", new { ErrorMessage = e.Message });
            }

            return View(AllFlights);
        }
    }
}