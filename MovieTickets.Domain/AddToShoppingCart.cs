using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTickets.Domain
{
    public class AddToShoppingCart
    {
        public Movie SelectedMovie { get; set; }
        public Guid MovieId { get; set; }
        public int Quantity { get; set; }
    }
}
