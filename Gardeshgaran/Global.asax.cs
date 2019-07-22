using AutoMapper;
using System.Web.Mvc;
using System.Web.Routing;
using Repository;
using Repository.Models.ViewModels;
using Repository.Models.Entity;

namespace Gardeshgaran
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ConfigMapper();
        }
        public void ConfigMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SP_CharterFlights_GetAvailability_Result, FlightViewModel>();
                cfg.CreateMap<SP_Persons_GetFlightTickets_Result, Ticket>();
            });
        }
    }
}
