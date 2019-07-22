using Repository.Models.Entity;
using System.Collections.Generic;

namespace Repository.Models.ViewModels
{
    public class TicketViewModel
    {
        public List<Ticket> Before { get; set; }
        public List<Ticket> Later { get; set; }

        public TicketViewModel()
        {
            Before = new List<Ticket>();
            Later = new List<Ticket>();
        }
    }
}
