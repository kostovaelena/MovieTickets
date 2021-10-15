using MovieTickets.Domain;
using Microsoft.Extensions.Logging;
using MovieTickets.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieTickets.Repository.Interface;



namespace MovieTickets.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Movie> _productRepository;
        private readonly IRepository<MovieInShoppingCart> _productInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IRepository<Movie> productRepository, ILogger<ProductService> logger, IRepository<MovieInShoppingCart> productInShoppingCartRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
            _logger = logger;
        }

        public bool AddToShoppingCart(AddToShoppingCart item, string userID)
        {

            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.UserCart;

            if (item.MovieId != null && userShoppingCard != null)
            {
                var product = this.GetDetailsForProduct(item.MovieId);

                if (product != null)
                {
                    MovieInShoppingCart itemToAdd = new MovieInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        Movie = product,
                        MovieId = product.Id,
                        ShoppingCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    this._productInShoppingCartRepository.Insert(itemToAdd);
                    _logger.LogInformation("Product was successfully added into ShoppingCart");
                    return true;
                }
                return false;
            }
            _logger.LogInformation("Something was wrong. ProductId or UserShoppingCard may be unaveliable!");
            return false;
        }


        public List<Movie> GetFilteredTickets()
        {
            DateTime now = DateTime.Now;

            return this._productRepository.GetAll().Where(t => (t.ValidDate.CompareTo(now) > 0) || (t.ValidDate.CompareTo(now) == 0)).ToList();

        }

        public List<Movie> FilterByGenre(Zanr genre)
        {
            return this._productRepository.GetAll().Where(t => t.Zanr.Equals(genre)).ToList();
        }

        public void CreateNewProduct(Movie p)
        {
            this._productRepository.Insert(p);
        }

        public void DeleteProduct(Guid id)
        {
            var product = this.GetDetailsForProduct(id);
            this._productRepository.Delete(product);
        }

        public List<Movie> GetAllProducts()
        {
            _logger.LogInformation("GetAllProducts was called!");
            return this._productRepository.GetAll().ToList();
        }

        public Movie GetDetailsForProduct(Guid? id)
        {
            return this._productRepository.Get(id);
        }

        public AddToShoppingCart GetShoppingCartInfo(Guid? id)
        {
            var product = this.GetDetailsForProduct(id);
            AddToShoppingCart model = new AddToShoppingCart
            {
                SelectedMovie = product,
                MovieId = product.Id,
                Quantity = 1
            };

            return model;
        }

        public void UpdeteExistingProduct(Movie p)
        {
            this._productRepository.Update(p);
        }
    }
}
