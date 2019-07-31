using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Constant
{
    /// <summary>
    /// Constant values for session keys
    /// </summary>
    public class SessionKey
    {
        /* USER INFO
        */
        public static readonly string UserID = "UserID";
        public static readonly string UserFullName = "UserFullName";
        public static readonly string ServiceProvider = "ServiceProvider";


        /* SELECTED FLIGHT INFO
         */
        public static readonly string OriginID = "OriginID";
        public static readonly string DestinationID = "DestinationID";
        public static readonly string DepartureDate = "DepartureDate";
        public static readonly string ReturnDate = "ReturnDate";
        public static readonly string FlightClass = "FlightClass";
        public static readonly string InfantCount = "InfantCount";
        public static readonly string ChildrenCount = "ChildrenCount";
        public static readonly string AdultCount = "AdultCount";

    }
}
