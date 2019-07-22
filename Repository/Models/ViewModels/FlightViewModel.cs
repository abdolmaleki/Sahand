using System;

namespace Repository.Models.ViewModels
{
    public class FlightViewModel
    {
        public string ServiceProvider { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string DepartureFlightID { get; set; }
        public string DepartureFlightNo { get; set; }
        public string DepartureFlightDate { get; set; }
        public string DepartureFlightTime { get; set; }
        public string ArrivalFlightDate { get; set; }
        public string ArrivalFlightTime { get; set; }
        public string AirLineName { get; set; }
        public string EAirLineName { get; set; }
        public short Capacity { get; set; }
        public Nullable<int> PassengersCount { get; set; }
        public decimal AdultSalePrice { get; set; }
        public decimal ChildSalePrice { get; set; }
        public decimal InfantSalePrice { get; set; }
        public decimal Commission { get; set; }
        public short MaxSale { get; set; }
        public bool IsAvailable { get; set; }
        public bool InternetSaleAllowable { get; set; }
        public string FlightClass { get; set; }
        public byte DirectionType { get; set; }
        public string Remarks { get; set; }
        public byte CharterStatus { get; set; }
        public bool RoundTrip { get; set; }
        public bool SOTO { get; set; }
        public bool OpenTicket { get; set; }
        public string AirplaneName { get; set; }
        public string EAirplaneName { get; set; }
        public string IATACode { get; set; }

        public FlightViewModel()
        {

        }

        public FlightViewModel(string serviceProvider, string fromCity, string toCity, string departureFlightID, string departureFlightNo, string departureFlightDate, string departureFlightTime, string arrivalFlightDate, string arrivalFlightTime, string airLineName, string eAirLineName, short capacity, int? passengersCount, decimal adultSalePrice, decimal childSalePrice, decimal infantSalePrice, decimal commission, short maxSale, bool isAvailable, bool internetSaleAllowable, string flightClass, byte directionType, string remarks, byte charterStatus, bool roundTrip, bool sOTO, bool openTicket, string airplaneName, string eAirplaneName, string iATACode)
        {
            ServiceProvider = serviceProvider;
            FromCity = fromCity;
            ToCity = toCity;
            DepartureFlightID = departureFlightID;
            DepartureFlightNo = departureFlightNo;
            DepartureFlightDate = departureFlightDate;
            DepartureFlightTime = departureFlightTime;
            ArrivalFlightDate = arrivalFlightDate;
            ArrivalFlightTime = arrivalFlightTime;
            AirLineName = airLineName;
            EAirLineName = eAirLineName;
            Capacity = capacity;
            PassengersCount = passengersCount;
            AdultSalePrice = adultSalePrice;
            ChildSalePrice = childSalePrice;
            InfantSalePrice = infantSalePrice;
            Commission = commission;
            MaxSale = maxSale;
            IsAvailable = isAvailable;
            InternetSaleAllowable = internetSaleAllowable;
            FlightClass = flightClass;
            DirectionType = directionType;
            Remarks = remarks;
            CharterStatus = charterStatus;
            RoundTrip = roundTrip;
            SOTO = sOTO;
            OpenTicket = openTicket;
            AirplaneName = airplaneName;
            EAirplaneName = eAirplaneName;
            IATACode = iATACode;
        }
    }
}
