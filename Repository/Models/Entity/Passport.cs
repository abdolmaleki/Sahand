using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.Entity
{
    public class Passport
    {
        public string Nationality { get; set; }
        public string PassportID { get; set; }
        public string ExpireDate { get; set; }
        public string NationalCode { get; set; }
    }
}
