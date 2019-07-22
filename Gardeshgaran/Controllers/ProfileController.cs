using AutoMapper;
using Repository;
using Repository.Models.Entity;
using Repository.Models.ViewModels;
using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;
using Utility.Constant;

namespace Gardeshgaran.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult GetTicketsTickets(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TicketViewModel ticketViewModel = new TicketViewModel();
                    using (GRGEntity context = new GRGEntity())
                    {
                        int PersonID = (int)Session[SessionKey.UserID];
                        DateTime From;
                        DateTime To;
                        if (FromDate == null && ToDate == null)
                        {
                            From = DateTime.Today.AddYears(1);
                            To = DateTime.Today.AddMonths(-3);
                        }
                        else
                        {
                            From = FromDate;
                            To = ToDate;
                        }
                        ObjectResult<SP_Persons_GetFlightTickets_Result> beforeTickets = context.SP_Persons_GetFlightTickets(PersonID, From, DateTime.Now);
                        ObjectResult<SP_Persons_GetFlightTickets_Result> laterTickets = context.SP_Persons_GetFlightTickets(PersonID, DateTime.Now, To);

                        foreach (SP_Persons_GetFlightTickets_Result item in beforeTickets.AsEnumerable())
                        {
                            ticketViewModel.Before.Add(Mapper.Map<Ticket>(item));
                        }

                        foreach (SP_Persons_GetFlightTickets_Result item in laterTickets.AsEnumerable())
                        {
                            ticketViewModel.Later.Add(Mapper.Map<Ticket>(item));
                        }
                    }
                }

                else
                {
                    ViewBag.IsValidModelState = false;
                }
            }

            catch (Exception e)
            {
                return RedirectToAction("HTTP500", "Error", new { ErrorMessage = e.Message });
            }
            return View();
        }
        public ActionResult ContactUS()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactUS(ContactViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                using (GRGEntity context = new GRGEntity())
                {
                }
            }

            else
            {
                ViewBag.IsValidModelState = false;
            }

            return View(ViewModel);
        }
    }
}