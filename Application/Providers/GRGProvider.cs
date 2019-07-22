using Application.Services;
using Repository;
using Repository.Models.Entity;
using Repository.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;
using Utility.Constant;
using Utility.Helper;

namespace Application.Provider
{
    public class GRGProvider
    {
        public static ServiceResponseModel GetFlights(FormCollection searchForm)
        {
            List<FlightViewModel> AllFlights = new List<FlightViewModel>();

            string Direction = searchForm["Direction"].ToString();
            string Origin = searchForm["Origin"].ToString();
            string Destination = searchForm["Destination"].ToString();
            string DepartureDate = searchForm["DepartureDate"].ToString();
            string ReturnDate = searchForm["ReturnDate"].ToString();
            string CabinType = searchForm["CabinType"].ToString();
            int Adult = Convert.ToInt16(searchForm["Adult"]);
            int Children = Convert.ToInt16(searchForm["Children"]);
            int Infant = Convert.ToInt16(searchForm["Infant"]);


            using (GRGEntity context = new GRGEntity())
            {
                int passangerNumber = Adult + Children;
                DateTime depDate = new DateTime(2019, 6, 28);
                DateTime retDate = new DateTime(2019, 6, 23);
                ObjectResult<SP_CharterFlights_GetAvailability_Result> DBFlights = context.SP_CharterFlights_GetAvailability(Origin, Destination, depDate, null, 1, passangerNumber, null, null);
                foreach (SP_CharterFlights_GetAvailability_Result item in DBFlights.AsEnumerable())
                {
                    FlightViewModel viewModel = AutoMapper.Mapper.Map<FlightViewModel>(item);
                    AllFlights.Add(viewModel);
                }
            }
            return new ServiceResponseModel(AllFlights, null);

        }
        public static ServiceResponseModel GetCustomFlightInfo(int CharterFlightID)
        {

            int Result = -1;
            ObjectParameter objFlightID = new ObjectParameter("FlightID", typeof(int));
            ObjectParameter objLinkCharterID = new ObjectParameter("LinkCharterID", typeof(int));
            ObjectParameter objCapacity = new ObjectParameter("Capacity", typeof(int));
            ObjectParameter objAdultSalePrice = new ObjectParameter("AdultSalePrice", typeof(decimal));
            ObjectParameter objChildSalePrice = new ObjectParameter("ChildSalePrice", typeof(decimal));
            ObjectParameter objInfantSalePrice = new ObjectParameter("InfantSalePrice", typeof(decimal));
            ObjectParameter objIsAvailable = new ObjectParameter("IsAvailable", typeof(bool));
            ObjectParameter objCommission = new ObjectParameter("Commission", typeof(int));
            ObjectParameter objMaxSale = new ObjectParameter("MaxSale", typeof(int));
            ObjectParameter objInternetSaleAllowable = new ObjectParameter("InternetSaleAllowable", typeof(bool));
            ObjectParameter objExchangeUnit = new ObjectParameter("ExchangeUnit", typeof(string));
            ObjectParameter objAdultSalePriceEx = new ObjectParameter("AdultSalePriceEx", typeof(decimal));
            ObjectParameter objInfantSalePriceEx = new ObjectParameter("InfantSalePriceEx", typeof(decimal));
            ObjectParameter objChildSalePriceEx = new ObjectParameter("ChildSalePriceEx", typeof(decimal));
            ObjectParameter objFlightClass = new ObjectParameter("FlightClass", typeof(string));
            ObjectParameter objRemarks = new ObjectParameter("Remarks", typeof(string));
            ObjectParameter objCharterStatus = new ObjectParameter("CharterStatus", typeof(string));
            ObjectParameter objExtraSalePrice = new ObjectParameter("ExtraSalePrice", typeof(decimal));
            ObjectParameter objExtraSalePriceEx = new ObjectParameter("ExtraSalePriceEx", typeof(decimal));
            ObjectParameter objOneWayAddPrice = new ObjectParameter("OneWayAddPrice", typeof(decimal));
            ObjectParameter objOneWayAddPriceEx = new ObjectParameter("OneWayAddPriceEx", typeof(decimal));
            ObjectParameter objRoundTrip = new ObjectParameter("RoundTrip", typeof(bool));
            ObjectParameter objSOTO = new ObjectParameter("SOTO", typeof(bool));
            ObjectParameter objOpenTicket = new ObjectParameter("OpenTicket", typeof(bool));

            ////////////////////////////////////////////////////////////////////////////////////////

            ObjectParameter flightNo = new ObjectParameter("flightNo", typeof(string));
            ObjectParameter fromCityID = new ObjectParameter("fromCityID", typeof(int));
            ObjectParameter toCityID = new ObjectParameter("toCityID", typeof(int));
            ObjectParameter flightTime = new ObjectParameter("flightTime", typeof(DateTime));
            ObjectParameter arrivalTime = new ObjectParameter("arrivalTime", typeof(DateTime));
            ObjectParameter airLineID = new ObjectParameter("airLineID", typeof(int));
            ObjectParameter flightType = new ObjectParameter("flightType", typeof(byte));
            ObjectParameter directionType = new ObjectParameter("directionType", typeof(byte));
            ObjectParameter capacity = new ObjectParameter("capacity", typeof(int));
            ObjectParameter airplaneID = new ObjectParameter("airplaneID", typeof(int));
            ObjectParameter adultPrice = new ObjectParameter("adultPrice", typeof(decimal));
            ObjectParameter childPrice = new ObjectParameter("childPrice", typeof(decimal));
            ObjectParameter infantPrice = new ObjectParameter("infantPrice", typeof(decimal));
            ObjectParameter passengersCount = new ObjectParameter("passengersCount", typeof(int));
            ObjectParameter virtualCapacity = new ObjectParameter("virtualCapacity", typeof(int));
            ObjectParameter fromAirportID = new ObjectParameter("fromAirportID", typeof(int));
            ObjectParameter toAirportID = new ObjectParameter("toAirportID", typeof(int));
            ObjectParameter bag = new ObjectParameter("bag", typeof(string));
            ObjectParameter flightClass = new ObjectParameter("flightClass", typeof(string));

            using (GRGEntity ctx = new GRGEntity())
            {
                Result = ctx.SP_CharterFlights_Get(CharterFlightID, objFlightID, objLinkCharterID, objCapacity, objAdultSalePrice, objChildSalePrice, objInfantSalePrice, objIsAvailable, objCommission, objMaxSale, objInternetSaleAllowable, objExchangeUnit, objAdultSalePriceEx, objChildSalePriceEx, objInfantSalePriceEx, objFlightClass, objRemarks, objCharterStatus, objExtraSalePrice, objExtraSalePriceEx, objOneWayAddPrice, objOneWayAddPriceEx, objRoundTrip, objSOTO, objOpenTicket);

                if (objFlightID.Value != null)
                {
                    ctx.SP_Flights_Get(CharterFlightID, flightNo, fromCityID, toCityID, flightTime, arrivalTime, airLineID, flightType, directionType, capacity, airplaneID, adultPrice, childPrice, infantPrice, passengersCount, virtualCapacity, fromAirportID, toAirportID, bag, flightClass);
                }
                else
                {
                    return null;
                }

                City FromCity = GetCityByID((int)fromCityID.Value);
                City ToCity = GetCityByID((int)toCityID.Value);
                Airline AirLine = GetAirLine((int)airLineID.Value);
                Airplane Airplane = GetAirplane((int)airplaneID.Value);
                FlightViewModel flightViewModel = new FlightViewModel(ServiceProvider.GRG, FromCity.CityName, ToCity.CityName, Convert.ToString(CharterFlightID), flightNo.Value.ToString(), ((DateTime)flightTime.Value).ToPersianDate(), ((DateTime)flightTime.Value).ToPersianTime(), ((DateTime)arrivalTime.Value).ToPersianTime(), ((DateTime)arrivalTime.Value).ToPersianTime(), AirLine.airLineName, AirLine.eAirLineName, (byte)capacity.Value, (int)passengersCount.Value, (decimal)objAdultSalePrice.Value, (decimal)objChildSalePrice.Value, (decimal)objInfantSalePrice.Value, (decimal)objCommission.Value, (short)objMaxSale.Value, (bool)objIsAvailable.Value, (bool)objInternetSaleAllowable.Value, objFlightClass.Value.ToString(), (byte)directionType.Value, objRemarks.Value.ToString(), (byte)objCharterStatus.Value, (bool)objRoundTrip.Value, (bool)objSOTO.Value, (bool)objOpenTicket.Value, "", "", AirLine.iATACode);
                return new ServiceResponseModel(flightViewModel, null);
            }

        }
        public static ServiceResponseModel FlightReservation(int personID, byte reservationType, int numAdultPerson, int numChildPerson, int numInfantPerson, int departureFlightID, int departureCharterFlightID, int returnFlightID, int returnCharterFlightID, bool openTicket, int countryPosition)
        {

            ObjectParameter objReservationID = new ObjectParameter("ReservationID", typeof(int));
            ObjectParameter objReturnMessage = new ObjectParameter("ReturnMessage", typeof(string));


            using (GRGEntity ctx = new GRGEntity())
            {
                ctx.SP_Reservations_InsertForFlightTicket(objReservationID, personID, reservationType, numAdultPerson, numChildPerson, numInfantPerson, departureFlightID, departureCharterFlightID, returnFlightID, returnCharterFlightID, openTicket, countryPosition, objReturnMessage);
            }
            if ((int)objReservationID.Value > 0)
            {
                return new ServiceResponseModel(objReservationID, null);
            }
            else
            {
                return new ServiceResponseModel(null, objReturnMessage.Value.ToString());
            }

        }
        public static ServiceResponseModel AddPassanger(int ReservationID, AddPassangerViewModel PassangerViewModel)

        {
            ObjectParameter objPersonID = new ObjectParameter("PersonID", typeof(int));

            using (GRGEntity ctx = new GRGEntity())
            {
                if (PassangerViewModel != null || PassangerViewModel.Passangers.Count > 0)
                {
                    foreach (Passanger item in PassangerViewModel.Passangers)
                    {
                        int result = ctx.SP_ReservationsPersons_Insert(objPersonID, ReservationID, item.EFirstName, item.ELastNAme, "", "", item.NationalID, item.PassportID, item.BirthDay_Year, (item.Gender == 1) ? true : false, item.Mobile, item.Tell, item.Email, item.Address, DateTime.Now, item.Nationality, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "");
                    }

                    return new ServiceResponseModel(ReservationID, null);
                }
                else
                {
                    return new ServiceResponseModel(null, "");
                }
            }
        }
        public static int CompleteReservation(int reservationID)
        {
            int result = -1;
            try
            {
                using (GRGEntity ctx = new GRGEntity())
                {
                    result = ctx.SP_Reservations_Complete(reservationID, true);
                }
                return result;
            }
            catch (Exception e)
            {
                return -1;
            }

        }
        private static int ConfirmReservation(int ReservationID, int Pid)
        {
            try
            {
                using (GRGEntity context = new GRGEntity())
                {
                    ObjectParameter objAgreementID = new ObjectParameter("agreementID", typeof(int));
                    context.SP_Reservations_Confirm(ReservationID, Pid, objAgreementID);

                    if ((int)objAgreementID.Value > 0)
                    {
                        return (int)objAgreementID.Value;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReservationID"></param>
        /// <param name="PersonID">if personId is null pass 1000 for this parameter</param>
        /// <returns></returns>
        public static ServiceResponseModel Booking(int ReservationID, int PersonID)

        {
            int aggrimantNumber = ConfirmReservation(ReservationID, PersonID);

            if (aggrimantNumber <= 0)
            {
                return new ServiceResponseModel(null, Utility.Locale.Message.CONFIRM_RESERVATION_ERROR);

            }
            else
            {
                int pnr = CompleteReservation(ReservationID);

                if (pnr <= 0)
                {
                    return new ServiceResponseModel(null, Utility.Locale.Message.COMPLETE_RESERVATION_ERROR);
                }
            }

            return new ServiceResponseModel(aggrimantNumber, null);

        }
        private static City GetCityByID(int cityID)
        {
            try
            {
                using (GRGEntity context = new GRGEntity())
                {
                    ObjectParameter cityName = new ObjectParameter("cityName", typeof(string));
                    ObjectParameter eCityName = new ObjectParameter("eCityName", typeof(string));
                    ObjectParameter countryID = new ObjectParameter("countryID", typeof(int));
                    ObjectParameter countryName = new ObjectParameter("countryName", typeof(string));
                    ObjectParameter eCountryName = new ObjectParameter("eCountryName", typeof(string));
                    ObjectParameter telPreCode = new ObjectParameter("telPreCode", typeof(string));
                    ObjectParameter abbreviation = new ObjectParameter("abbreviation", typeof(string));
                    context.SP_City_Get(cityID, cityName, eCityName, countryID, countryName, eCountryName, telPreCode, abbreviation);
                    return new City(cityName.Value.ToString(), eCityName.Value.ToString(), countryName.Value.ToString(), eCountryName.Value.ToString(), telPreCode.Value.ToString(), abbreviation.Value.ToString());
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private static Airline GetAirLine(int airLineID)
        {
            ObjectParameter airLineName = new ObjectParameter("airLineName", typeof(string));
            ObjectParameter eAirLineName = new ObjectParameter("eAirLineName", typeof(string));
            ObjectParameter officeID = new ObjectParameter("officeID", typeof(int));
            ObjectParameter commissionDomestic = new ObjectParameter("commissionDomestic", typeof(decimal));
            ObjectParameter commissionInternational = new ObjectParameter("commissionInternational", typeof(decimal));
            ObjectParameter commissionDomesticRefund = new ObjectParameter("commissionDomesticRefund", typeof(decimal));
            ObjectParameter commissionInternationalRefund = new ObjectParameter("commissionInternationalRefund", typeof(decimal));
            ObjectParameter iATACode = new ObjectParameter("iATACode", typeof(string));
            ObjectParameter rTRDNo = new ObjectParameter("rTRDNo", typeof(string));

            using (GRGEntity context = new GRGEntity())
            {
                context.SP_AirLines_Get(airLineID, airLineName, eAirLineName, officeID, commissionDomestic, commissionInternational, commissionDomesticRefund, commissionInternationalRefund, iATACode, rTRDNo);
                return new Airline(airLineName.Value.ToString(), eAirLineName.Value.ToString(), (int)officeID.Value, iATACode.Value.ToString());
            }
        }
        private static Airplane GetAirplane(int airplaneID)
        {
            ObjectParameter airplaneName = new ObjectParameter("airplaneName", typeof(string));
            ObjectParameter eAirplaneName = new ObjectParameter("eAirplaneName", typeof(string));

            using (GRGEntity context = new GRGEntity())
            {
                context.SP_Airplanes_Get(airplaneID, airplaneName, eAirplaneName);
                return new Airplane(airplaneName.Value.ToString(), eAirplaneName.Value.ToString());
            }
        }
    }
}