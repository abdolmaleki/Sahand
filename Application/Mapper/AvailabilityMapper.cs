using Application.AtlasjetService;
using Repository.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class AvailabilityMapper
    {
        /// <summary>
        /// Convert atlasject avalaibility to flightviewModel
        /// </summary>
        /// <param name="availability"></param>
        /// <returns></returns>
        public static List<FlightViewModel> ConvertAtlasjetAvailabilityToViewModels(AvailabilityData availability)
        {
            List<FlightViewModel> AllFlights = new List<FlightViewModel>();

            foreach (AvailabilityFlightData flight in availability.flightData)
            {
                AvailabilitySegmentData flightSegment = flight.segmentData[0];

                foreach (AvailabilityClassData flightClass in flight.classData)
                {
                    FlightViewModel viewModel = new FlightViewModel
                    {

                        AdultSalePrice = (decimal)flightClass.adultprice,
                        AirLineName = flight.segmentData[0].arrdesc,
                        AirplaneName = "",
                        ArrivalFlightDate = flightSegment.arrdate,
                        ArrivalFlightTime = flightSegment.arrtime,
                        Capacity = (short)flightClass.seat,
                        CharterStatus = 1,
                        ChildSalePrice = (decimal)flightClass.childprice,
                        Commission = 0,
                        DepartureFlightDate = flightSegment.depdate,
                        DepartureFlightNo = flightSegment.flightno,
                        DepartureFlightID = flight.voyagecode,
                        DepartureFlightTime = flightSegment.deptime,
                        EAirLineName = flight.segmentData[0].arrdesc,
                        EAirplaneName = "",
                        FlightClass = flightClass.classdesc,
                        FromCity = flightSegment.depcode,
                        InfantSalePrice = (decimal)flightClass.infprice,
                        ToCity = flightSegment.arrcode,
                    };

                    AllFlights.Add(viewModel);
                }
            }

            return AllFlights;
        }
    }
}
