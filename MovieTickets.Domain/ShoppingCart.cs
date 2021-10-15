using MovieTickets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTickets.Domain
{
    public class ShoppingCart : BaseEntity 
    {
      
        public virtual ICollection<MovieInShoppingCart> MovieInShoppingCarts { get; set; }
        public MTApplicationUser Owner { get; set; }
        public string OwnerId { get; set; }

    }
}
