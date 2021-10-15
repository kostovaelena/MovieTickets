using MovieTickets.Domain;
using System;
using System.Collections.Generic;
using System.Text;


namespace MovieTickets.Services.Interface
{
    public interface IProductService
    {
        List<Movie> GetAllProducts();
        Movie GetDetailsForProduct(Guid? id);
        void CreateNewProduct(Movie p);
        void UpdeteExistingProduct(Movie p);
        AddToShoppingCart GetShoppingCartInfo(Guid? id);
        void DeleteProduct(Guid id);
        bool AddToShoppingCart(AddToShoppingCart item, string userID);
        List<Movie> GetFilteredTickets();
        List<Movie> FilterByGenre(Zanr genre);

    }
}
