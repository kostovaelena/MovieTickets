using MovieTickets.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTickets.Domain
{

    public enum Zanr
    {
        ADVENTURE,
        ROMANCE,
        DRAMA,
        FANTASY
    }

    public class Movie : BaseEntity
    {
       

        [Required]
        public string Image { get; set; }
        [Required]
        [DisplayName("Оригинален наслов")]
        public string Name { get; set; }
        
        [Required]
        [DisplayName("Содржина")]
        public string Description { get; set; }
        
        [Required]
        [DisplayName("Траење на филмот")]
        public int Traenje { get; set; }
        [Required]
        [DisplayName("Земја")]
        public string Zemja { get; set; }
        [Required]
        [DisplayName("Година")]
        public int Godina { get; set; }
        [Required]
        [DisplayName("Жанр")]
        public Zanr Zanr { get; set; }
        [Required]
        [DisplayName("Актери")]
        public string Akteri { get; set; }

        [Required]
        [DisplayName("Категорија")]
        public string Kategorija { get; set; }
        [Required]
        [DisplayName("Рејтинг")]
        public double Rating { get; set; }

        [Required]
        [DisplayName("Цена")]
        public int Price { get; set; }


        [Required]
        [DisplayName("Датум и време на на емитување")]
        public DateTime ValidDate { get; set; }

       



        public virtual ICollection<MovieInShoppingCart> MovieInShoppingCarts { get; set; }
        public IEnumerable<MovieInOrder> MovieInOrders { get; set; }






    }
}
