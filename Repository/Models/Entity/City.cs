using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Entity
{
    public class City
    {
        public string CityName { get; set; }
        public string ECityName { get; set; }
        public string CountryName { get; set; }
        public string ECountryName { get; set; }
        public string TelPreCode { get; set; }
        public string Abbreviation { get; set; }

        public City(string cityName, string eCityName, string countryName, string eCountryName, string telPreCode, string abbreviation)
        {
            CityName = cityName;
            ECityName = eCityName;
            CountryName = countryName;
            ECountryName = eCountryName;
            TelPreCode = telPreCode;
            Abbreviation = abbreviation;
        }
    }
}
