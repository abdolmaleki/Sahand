using Repository.Models.Entity;
using System.Collections.Generic;

namespace Repository.Models.ViewModels
{
    public class AddPassangerViewModel
    {
        public List<Passanger> Passangers { get; set; }
        public Passport Passport { get; set; }
        public string Country { get; set; }
        public string Tell { get; set; }
        public string Email { get; set; }
        public string EmailConfirm { get; set; }
        public string UnitPrice { get; set; }
        public string TotalPrice { get; set; }
        public string Tax { get; set; }

    }
}
