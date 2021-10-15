
using MovieTickets.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieTickets.Repository;
using MovieTickets.Domain;


namespace MovieTickets.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<MTApplicationUser> entities;
        string errorMessage = string.Empty;
        

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<MTApplicationUser>();
        }
        public IEnumerable<MTApplicationUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public MTApplicationUser Get(string id)
        {
            return entities
               .Include(z => z.UserCart)
               .Include("UserCart.MovieInShoppingCarts")
               .Include("UserCart.MovieInShoppingCarts.Movie")
               .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(MTApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(MTApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(MTApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        
    }
}
