using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieTickets.Domain;

using System;

namespace MovieTickets.Repository
{
    public class ApplicationDbContext : IdentityDbContext<MTApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<MovieInShoppingCart> MovieInShoppingCarts { get; set; }
        public virtual DbSet<MovieInOrder> MovieInOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

           /* builder.Entity<MovieInShoppingCart>()
                .HasKey(z => new { z.MovieId, z.ShoppingCartId });*/

            builder.Entity<MovieInShoppingCart>()
               .HasOne(z => z.Movie)
               .WithMany(z => z.MovieInShoppingCarts)
               .HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<MovieInShoppingCart>()
              .HasOne(z => z.ShoppingCart)
              .WithMany(z => z.MovieInShoppingCarts)
              .HasForeignKey(z => z.MovieId);

            builder.Entity<ShoppingCart>()
              .HasOne<MTApplicationUser>(z => z.Owner)
              .WithOne(z => z.UserCart)
              .HasForeignKey<ShoppingCart>(z => z.OwnerId);

/*
            builder.Entity<MovieInOrder>()
              .HasKey(z => new { z.MovieId, z.OrderId });*/

            builder.Entity<MovieInOrder>()
                .HasOne(z => z.OrderedMovie)
                .WithMany(z => z.MovieInOrders)
                .HasForeignKey(z => z.OrderId);

            builder.Entity<MovieInOrder>()
                .HasOne(z => z.UserOrder)
                .WithMany(z => z.MovieInOrders)
                .HasForeignKey(z => z.MovieId);
        }
    }
}
