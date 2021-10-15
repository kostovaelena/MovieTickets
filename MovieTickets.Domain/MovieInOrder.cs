using MovieTickets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTickets.Domain
{
    public class MovieInOrder : BaseEntity
    {
        public Guid MovieId { get; set; }
        public Movie OrderedMovie { get; set; }

        public Guid OrderId { get; set; }
        public Order UserOrder { get; set; }
        public int Quantity { get; set; }

    }
}
