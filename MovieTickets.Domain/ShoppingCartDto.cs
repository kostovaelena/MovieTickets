using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTickets.Domain
{
    public class ShoppingCartDto
    {
        public List<MovieInShoppingCart> Movies { get; set; }
        public double TotalPrice { get; set; }
    }
}
