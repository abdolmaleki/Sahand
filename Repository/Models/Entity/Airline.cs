using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Entity
{
    public class Airline
    {
        public string airLineName { get; set; }
        public string eAirLineName { get; set; }
        public int officeID { get; set; }
        public string iATACode { get; set; }
        public Airline(string airLineName, string eAirLineName, int officeID, string iATACode)
        {
            this.airLineName = airLineName;
            this.eAirLineName = eAirLineName;
            this.officeID = officeID;
            this.iATACode = iATACode;
        }
    }
}
