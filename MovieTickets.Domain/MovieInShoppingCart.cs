using MovieTickets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTickets.Domain
{
    public class MovieInShoppingCart : BaseEntity
    {
        public Guid MovieId { get; set; }
        public Guid ShoppingCartId { get; set; }
        public Movie Movie { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int Quantity { get; set; }

    }
}
