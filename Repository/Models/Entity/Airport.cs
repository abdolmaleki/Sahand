using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Entity
{
    public class Airport
    {
        public string AirportName { get; set; }
        public string EAirportName { get; set; }
        public string Abbreviation { get; set; }
        public int CityID { get; set; }

        public Airport(string airportName, string eAirportName, string abbreviation, int cityID)
        {
            AirportName = airportName;
            EAirportName = eAirportName;
            Abbreviation = abbreviation;
            CityID = cityID;
        }
    }
}
