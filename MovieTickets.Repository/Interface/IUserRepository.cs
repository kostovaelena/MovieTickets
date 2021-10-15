
using MovieTickets.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTickets.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<MTApplicationUser> GetAll();
        MTApplicationUser Get(string id);
        void Insert(MTApplicationUser entity);
        void Update(MTApplicationUser entity);
        void Delete(MTApplicationUser entity);
    }
}
