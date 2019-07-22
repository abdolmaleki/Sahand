using Application.AtlasjetService;
using Application.Provider;
using Repository;
using Repository.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;
using Utility.Constant;

namespace Application.Services
{
    public class FlightService
    {
        private FlightService()
        {

        }
        private static FlightService flightService;
        public static FlightService GetInstance()
        {
            if (flightService == null)
            {
                flightService = new FlightService();
            }
            return flightService;
        }

        //    ____      _         _             _ _       _     _ _ _         
        //   / ___| ___| |_      / \__   ____ _(_) | __ _| |__ (_) | |_ _   _ 
        //  | |  _ / _ \ __|    / _ \ \ / / _` | | |/ _` | '_ \| | | __| | | |
        //  | |_| |  __/ |_    / ___ \ V / (_| | | | (_| | |_) | | | |_| |_| |
        //   \____|\___|\__|  /_/   \_\_/ \__,_|_|_|\__,_|_.__/|_|_|\__|\__, |
        //                                                              |___/ 

        public ServiceResponseModel GetAvailablity(FormCollection searchForm)
        {
            List<FlightViewModel> flightViewModels = new List<FlightViewModel>();
            ServiceResponseModel GRGresponse = GRGProvider.GetFlights(searchForm);
            if (GRGresponse.Data != null)
            {
                flightViewModels.AddRange((List<FlightViewModel>)GRGresponse.Data);

            }

            ServiceResponseModel AtlasjetResponse = AtlasJetProvider.GetAvailablity(searchForm);

            if (AtlasjetResponse.Data != null)
            {
                flightViewModels.AddRange((List<FlightViewModel>)AtlasjetResponse.Data);

            }
            return new ServiceResponseModel(flightViewModels, null);

        }
        public ServiceResponseModel GetAirportSelectList()

        {
            try
            {
                List<SelectListItem> airportList = new List<SelectListItem>();
                using (GRGEntity context = new GRGEntity())
                {
                    ObjectResult<SP_Airports_GetForWeb_Result> airpots = context.SP_Airports_GetForWeb();
                    foreach (SP_Airports_GetForWeb_Result item in airpots.AsEnumerable())
                    {
                        SelectListItem airport = new SelectListItem
                        {
                            Text = item.AirportName,
                            Value = item.Abbreviation
                        };

                        airportList.Add(airport);
                    }
                }

                return new ServiceResponseModel(airportList, null);

            }

            catch (Exception e)
            {
                return new ServiceResponseModel(null, Utility.Locale.Message.SERVER_CONNECTION_ERROR);
            }
        }

        //   ____                  _  __ _          _    _____ _ _       _     _      ___        __       
        //  / ___| _ __   ___  ___(_)/ _(_) ___  __| |  |  ___| (_) __ _| |__ | |_   |_ _|_ __  / _| ___  
        //  \___ \| '_ \ / _ \/ __| | |_| |/ _ \/ _` |  | |_  | | |/ _` | '_ \| __|   | || '_ \| |_ / _ \ 
        //   ___) | |_) |  __/ (__| |  _| |  __/ (_| |  |  _| | | | (_| | | | | |_    | || | | |  _| (_) |
        //  |____/| .__/ \___|\___|_|_| |_|\___|\__,_|  |_|   |_|_|\__, |_| |_|\__|  |___|_| |_|_|  \___/ 
        //        |_|                                              |___/                                  

        public ServiceResponseModel GetFlightInfo(string Provider, params string[] args)
        {
            switch (Provider)
            {
                case ServiceProvider.GRG:
                    return GRGProvider.GetCustomFlightInfo(Convert.ToInt16(args[0]));
                default:
                    return null;
            }

        }

        // ____              ____              _    _              
        //|  _ \ _ __ ___   | __ )  ___   ___ | | _(_)_ __   __ _  
        //| |_) | '__/ _ \  |  _ \ / _ \ / _ \| |/ / | '_ \ / _` | 
        //|  __/| | |  __/  | |_) | (_) | (_) |   <| | | | | (_| | 
        //|_|   |_|  \___|  |____/ \___/ \___/|_|\_\_|_| |_|\__, | 
        //                                                  |___/  

        public ServiceResponseModel PreBooking(string Provider, params object[] args)
        {
            switch (Provider)
            {
                case ServiceProvider.GRG:
                    return GRGProvider.FlightReservation((int)args[0], (byte)args[1], (int)args[2], (int)args[3], (int)args[4], (int)args[5], (int)args[6], (int)args[7], (int)args[1], (bool)args[8], (int)args[9]);
                default:
                    return new ServiceResponseModel();
            }
        }

        //     _       _     _    ____                                           
        //    / \   __| | __| |  |  _ \ __ _ ___ ___  __ _ _ __   __ _  ___ _ __ 
        //   / _ \ / _` |/ _` |  | |_) / _` / __/ __|/ _` | '_ \ / _` |/ _ \ '__|
        //  / ___ \ (_| | (_| |  |  __/ (_| \__ \__ \ (_| | | | | (_| |  __/ |   
        // /_/   \_\__,_|\__,_|  |_|   \__,_|___/___/\__,_|_| |_|\__, |\___|_|   
        //   

        /// <summary>
        /// Insert Passangers And return Reservation ID
        /// </summary>
        /// <param name="Provider">Service Provider</param>
        /// <param name="passangerViewModel">Passsangers Inforamtion</param>
        /// <param name="extraArgs">some extra information such as Reserv ID</param>
        /// <returns></returns>
        public ServiceResponseModel AddPassangers(string Provider, AddPassangerViewModel passangerViewModel, params string[] extraArgs)
        {
            switch (Provider)
            {
                case ServiceProvider.GRG:
                    return GRGProvider.AddPassanger(Convert.ToInt16(extraArgs[0]), passangerViewModel);
                case ServiceProvider.ATLAS_JET:
                    return AtlasJetProvider.AddPassenger(passangerViewModel, extraArgs[0], extraArgs[0], extraArgs[0], extraArgs[0], extraArgs[0], Convert.ToDouble(extraArgs[0]));

            }

            return new ServiceResponseModel(null, "");
        }

        //   ____              _    _              
        //  | __ )  ___   ___ | | _(_)_ __   __ _  
        //  |  _ \ / _ \ / _ \| |/ / | '_ \ / _` | 
        //  | |_) | (_) | (_) |   <| | | | | (_| | 
        //  |____/ \___/ \___/|_|\_\_|_| |_|\__, | 
        //                                  |___/  

        public ServiceResponseModel Booking(string Provider, params string[] bookingArgs)
        {
            switch (Provider)
            {
                case ServiceProvider.GRG:
                    return GRGProvider.Booking(Convert.ToInt32(bookingArgs[0]), Convert.ToInt32(bookingArgs[0]));
                case ServiceProvider.ATLAS_JET:
                    ServiceResponseModel response = AtlasJetProvider.Booking(Convert.ToInt32(bookingArgs[0]));
                    if (response.Data != null)
                    {
                        BookingData bookingData = (BookingData)response.Data;
                        if (!String.IsNullOrEmpty(bookingData.pnrnumber))
                        {
                            InsertFlightToSahand();
                            return response;
                        }
                    }
                    return null;
                default:
                    return null;
            }
        }

        public void InsertFlightToSahand()
        {

        }
    }
}
