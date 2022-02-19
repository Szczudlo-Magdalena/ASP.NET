using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using LibApp.Models;

namespace LibApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Genre>().HasData(
                new Genre()
                {
                    Id = 1,
                    Name = "Action"
                }, new Genre()
                {
                    Id = 2,
                    Name = "Thriller"
                }, new Genre()
                {
                    Id = 3,
                    Name = "Romance"
                }, new Genre()
                {
                    Id = 4,
                    Name = "Comedy"
                }, new Genre()
                {
                    Id = 5,
                    Name = "Reportage"
                }, new Genre()
                {
                    Id = 6,
                    Name = "Fantasy"
                }, new Genre()
                {
                    Id = 7,
                    Name = "Sci-Fi"
                }, new Genre()
                {
                    Id = 8,
                    Name = "IT"
                });

            builder.Entity<MembershipType>().HasData(
                new MembershipType()
                {
                    Id = 1,
                    Name = "Pay as You Go",
                    SignUpFee = 0,
                    DiscountRate = 0,
                    DurationInMonths = 0
                }, new MembershipType()
                {
                    Id = 2,
                    Name = "Monthly",
                    SignUpFee = 10,
                    DiscountRate = 10,
                    DurationInMonths = 1
                }, new MembershipType()
                {
                    Id = 3,
                    Name = "Quaterly",
                    SignUpFee = 40,
                    DiscountRate = 15,
                    DurationInMonths = 4
                }, new MembershipType()
                {
                    Id = 4,
                    Name = "Yearly",
                    SignUpFee = 120,
                    DiscountRate = 20,
                    DurationInMonths = 12
                });

            builder.Entity<Customer>().HasData(
                new Customer()
            {
                Id = 1,
                Name = "Jan Kowalski",
                Birthdate = DateTime.Parse("2000-01-01"),
                HasNewsletterSubscribed = true,
                MembershipTypeId = 4
            }, new Customer()
            {
                Id = 2,
                Name = "Zuzanna Nowak",
                Birthdate = DateTime.Parse("1999-01-01"),
                HasNewsletterSubscribed = false,
                MembershipTypeId = 1
            }, new Customer()
            {
                Id = 3,
                Name = "Józef Śmietana",
                Birthdate = DateTime.Parse("1990-07-21"),
                HasNewsletterSubscribed = true,
                MembershipTypeId = 1
            });

            builder.Entity<Book>().HasData(
                new Book()
                {
                    Id = 1,
                    Name = "Thinking in C++ vol. 1",
                    AuthorName = "Bruce Eckel",
                    DateAdded = DateTime.Now,
                    GenreId = 8,
                    NumberAvailable = 10,
                    NumberInStock = 7,
                    ReleaseDate = DateTime.Parse("1990-01-01")
                }, new Book()
                {
                    Id = 2,
                    Name = "Thinking in C++ vol. 2",
                    AuthorName = "Bruce Eckel",
                    DateAdded = DateTime.Now,
                    GenreId = 8,
                    NumberAvailable = 10,
                    NumberInStock = 8,
                    ReleaseDate = DateTime.Parse("1993-01-01")
                });

            builder.Entity<Rental>().HasData(
                new Rental()
                {
                    Id = 1,
                    BookId = 1,
                    CustomerId = 1,
                    DateRented = DateTime.Now
                },
                new Rental()
                {
                    Id = 2,
                    BookId = 1,
                    CustomerId = 2,
                    DateRented = DateTime.Now
                },
                new Rental()
                {
                    Id = 3,
                    BookId = 1,
                    CustomerId = 3,
                    DateRented = DateTime.Now
                });
        }
    }
}
