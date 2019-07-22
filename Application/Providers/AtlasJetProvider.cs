﻿using Application.AtlasjetService;
using Application.Mapper;
using Application.Services;
using Repository.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using Utility.Constant;
using Repository;

namespace Application.Provider
{
    public class AtlasJetProvider
    {
        private static AtlasjetClient client;

        public AtlasJetProvider()
        {
            client = new AtlasjetClient();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="lang">Language (EN,TR)</param>
        /// <param name="direction">Flight type (ONEWAY,ROUNDTRIP)</param>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="depdate">Departure Flight date (YYYYMMDD) 20160610</param>
        /// <param name="retdate">Return Flight date (YYYYMMDD) 20160620</param>
        /// <param name="adult">The number of adult passengers</param>
        /// <param name="child">The number of child passengers (2-12 between the ages)</param>
        /// <param name="inf">Baby passengers number (0-2 between the ages)</param>
        /// <param name="ogr">Student number of passengers</param>
        /// <param name="tsk">Soldier number of passengers</param>
        /// <param name="yp">The number of  young passengers (12-24between the ages)</param>
        /// <param name="sc">The number of older passenger (65 over  the age)</param>
        /// <param name="triptype">for Departure  : “DEP”,for Return “RET”, for Departure and Return “ALL”</param>
        /// <returns></returns>

        public static ServiceResponseModel GetAvailablity(FormCollection searchForm)
        {
            List<FlightViewModel> AllFlight = new List<FlightViewModel>();
            try
            {
                string userName = ConfigurationManager.AppSettings["AtlasjetUsername"].ToString();
                string password = ConfigurationManager.AppSettings["AtlasjetPassword"].ToString();
                string direction = searchForm["Direction"].ToString();
                string origin = searchForm["Origin"].ToString();
                string destination = searchForm["Destination"].ToString();
                string departureDate = searchForm["DepartureDate"].ToString();
                string returnDate = searchForm["ReturnDate"].ToString();
                string cabinType = searchForm["CabinType"].ToString();
                int adult = Convert.ToInt16(searchForm["Adult"]);
                int children = Convert.ToInt16(searchForm["Children"]);
                int infant = Convert.ToInt16(searchForm["Infant"]);

                AvailabilityData availability = client.availabilityV4(userName, password, Language.English, direction, origin, destination, departureDate, returnDate, adult, children, infant, 0, 0, 0, 0, "ALL", "");
                if (availability != null && availability.systemMessageData != null)
                {
                    AllFlight = AvailabilityMapper.ConvertAtlasjetAvailabilityToViewModels(availability);
                    return new ServiceResponseModel(AllFlight, null);
                }
                else
                {
                    return new ServiceResponseModel(null, availability.systemMessageData.ToString());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// The selected flights and passenger information system is processed, sent as a result of this, a reference number is given.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="locale"></param>
        /// <param name="direction"></param>
        /// <param name="depvoyagecode">The code used to represent the selected departure flight</param>
        /// <param name="depclass">The selected departure class</param>
        /// <param name="retvoyagecode">The selected code used to represent the return flight (one-way and open tickets can be null)</param>
        /// <param name="retclass">The selected return class (one-way or open tickets can be null)</param>
        /// <param name="faresamount"></param>
        /// <param name="passengersDatas"></param>
        /// <param name="spetypeid"></param>
        //// passenger types: ADLT Adult (12+)/ CTBL Child(Between 2-12) /INF Infant(Between 0-2)/ OGR Student(Only Cyprus Flights)/ TSK Soldier(Only Cyprus Flights)/ YP Young(Between 12-24)
        /// <returns></returns>
        public static ServiceResponseModel AddPassenger(AddPassangerViewModel PassangerViewModel, string direction, string depvoyagecode, string depclass, string retvoyagecode, string retclass, double? farsAmount)
        {
            try
            {
                ReferenceData refData;
                PassengersData[] passengersDatas = new PassengersData[PassangerViewModel.Passangers.Count];
                string userName = ConfigurationManager.AppSettings["AtlasjetUsername"].ToString();
                string password = ConfigurationManager.AppSettings["AtlasjetPassword"].ToString();
                refData = client.addPassengerV3(userName, password, Language.English, direction, depvoyagecode, depclass, retvoyagecode, retclass, farsAmount, passengersDatas, "");
                return new ServiceResponseModel(refData, null);
            }
            catch (Exception e)
            {
                return new ServiceResponseModel(null, e.Message);
            }
        }

        /// <summary>
        /// Booking process addPassenger method "getRefnumber ()" field as to be sent and rcvd sent field is done with refnumber.As a consequence will be the details are given below.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="locale"></param>
        /// <param name="refnumber">: addPassenger methodundan getRefnumber field in the returned value is.</param>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        public static ServiceResponseModel Booking(long refnumber)
        {
            try
            {
                string userName = ConfigurationManager.AppSettings["AtlasjetUsername"].ToString();
                string password = ConfigurationManager.AppSettings["AtlasjetPassword"].ToString();
                int campaignID = Convert.ToInt16(ConfigurationManager.AppSettings["AtlasjetCampaignID"]);
                BookingData bookingData = client.booking(userName, password, Language.English, refnumber, campaignID);
                return new ServiceResponseModel(bookingData.pnrnumber, null);
            }
            catch (Exception e)
            {
                return new ServiceResponseModel(null, e.Message);
            }
        }

        /// <summary>
        /// The Not Ticketed Booking Process (Pre Booking)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="locale"></param>
        /// <param name="refnumber"></param>
        /// <returns></returns>
        public PreBookingData PreBooking(string userName, string password, string locale, long refnumber)
        {
            try
            {
                return client.preBooking(userName, password, locale, refnumber);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="locale"></param>
        /// <param name="pnr"></param>
        /// <returns></returns>
        public BookingInfoData GetBookingInfo(string userName, string password, string locale, string pnr)
        {
            try
            {
                return client.bookingInfo(userName, password, locale, pnr);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="locale"></param>
        /// <param name="direction">One way, round trip (ONEWAY,ROUNDTRIP, OWROUNDTRIP)</param>
        /// <param name="depvoyagecode"></param>
        /// <param name="depclass"></param>
        /// <param name="retvoyagecode"></param>
        /// <param name="retclass"></param>
        /// <param name="adult">Adult passenger count </param>
        /// <param name="child">child passenger count (2-12)  </param>
        /// <param name="inf">Infant passenger count (0-2)</param>
        /// <param name="ogr">Student passenger count (Only Cyprus Flights) </param>
        /// <param name="tsk">Soldier passenger sayısı (Only Cyprus Flights) </param>
        /// <param name="yp">Young passenger count (12-24) </param>
        /// <param name="sc"></param>
        /// <param name="spetypeid">Special price code </param>
        /// <returns></returns>
        public FareQuoteData GetSelectedFlightInfo(string userName, string password, string locale, string direction, string depvoyagecode, string depclass, string retvoyagecode, string retclass, int adult, int child, int inf, int ogr, int tsk, int yp, int sc, string spetypeid)
        {
            try
            {
                return client.fares(userName, password, locale, direction, depvoyagecode, depclass, retvoyagecode, retclass, adult, child, inf, ogr, tsk, yp, sc, spetypeid);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string GetTableClassName(string code)
        {
            switch (code)
            {
                case "BFFLEX":
                    return "BUSINESS FULL FLEXIBLE";

                case "BSFLEX":
                    return "BUSINESS SEMI FLEXIBLE";

                case "BPROMO":
                    return "BUSINESS PROMO";

                case "EFULLFLEX":
                    return "ECONOMY FULL FLEXIBLE";

                case "ESEMIFLEX":
                    return "ECONOMY SEMI FLEXIBLE";

                case "ECOREST":
                    return "ECONOMY RESTRICTED";

                case "ECOPROMO":
                    return "ECONOMY PROMO";

                case "SBBLOCK":
                    return "SOFT BLOCK";

                case "HBBLOCK":
                    return "HARD BLOCK";
                default:
                    return "unknown";
            }
        }

        public class PassengerTypeCodes
        {
            public static string Adult = "ADLT";
            public static string Child = "CTBL";
            public static string Infant = "INF";
            public static string Student = "OGR";
            public static string Soldier = "TSK";
            public static string Young = "YP";
        }

        public class DirectionTypes
        {
            public static string ROUNDTRIP = "ROUNDTRIP";
            public static string ONEWAY = "ONEWAY";
            public static string OWROUNDTRIP = "OWROUNDTRIP";

        }

        public class TripTypes
        {
            public static string ROUNDTRIP = "ROUNDTRIP";
            public static string ONEWAY = "ONEWAY";
            public static string OWROUNDTRIP = "OWROUNDTRIP";

        }

    }
}