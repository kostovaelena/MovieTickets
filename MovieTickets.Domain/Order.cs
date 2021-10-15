using MovieTickets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTickets.Domain
{
    public class Order : BaseEntity
    {
        
        public string UserId { get; set; }
        public MTApplicationUser User { get; set; }

        public IEnumerable<MovieInOrder> MovieInOrders { get; set; }
    }
}
