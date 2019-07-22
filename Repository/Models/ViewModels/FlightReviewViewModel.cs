using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.ViewModels
{
    public class FlightReviewViewModel
    {
        public FlightViewModel DepartureFlight { get; set; }
        public FlightViewModel ReturnFlight { get; set; }
        public string UnitPrice { get; set; }
        public string TotalPrice { get; set; }
    }
}
