using System;

namespace Repository.Models.Entity
{
    public class Ticket
    {
        public string TicketNo { get; set; }
        public int AgreementID { get; set; }
        public int FlightID { get; set; }
        public string FlightNo { get; set; }
        public string FlightDate { get; set; }
        public string FlightTime { get; set; }
        public string AirLineName { get; set; }
        public string EAirLineName { get; set; }
        public string FromCityName { get; set; }
        public string FromCityNameEn { get; set; }
        public string FromCityAbbreviation { get; set; }
        public string ToCityName { get; set; }
        public string ToCityNameEn { get; set; }
        public string ToCityAbbreviation { get; set; }
        public System.DateTime IssueDate { get; set; }
        public string Class { get; set; }
        public string IssuantAgency { get; set; }
        public string StatusDesc { get; set; }
        public System.DateTime OriginalDate { get; set; }
        public string FlightDateFa { get; set; }
        public byte DirectionType { get; set; }
        public Nullable<int> FarePrice { get; set; }
        public Nullable<int> TotalPrice { get; set; }
        public int Nationality { get; set; }
        public string CountryName { get; set; }
        public string Price { get; set; }
        public byte TicketStatus { get; set; }
        public string FromAirportAbbreviation { get; set; }
        public string ToAirportAbbreviation { get; set; }
    }
}
