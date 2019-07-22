using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Entity
{
    public class Airplane
    {
        public string AirplaneName { get; set; }
        public string EAirplaneName { get; set; }
public Airplane(string airplaneName, string eAirplaneName)
        {
            AirplaneName = airplaneName;
            EAirplaneName = eAirplaneName;
        }
    }
}
