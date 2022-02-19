using LibApp.Dtos;
using LibApp.Models;
using LibApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LibApp.Controllers.Api
{
    // kontroler przetwarzający informacje o wypożyczeniach (API)
    [Route("api/[controller]")]
    [ApiController]
    public class NewRentalsController : ControllerBase
    {
        private readonly RentalRepository rentalRepository;
        private readonly CustomerRepository customerRepository;
        private readonly BookRepository bookRepository;

        public NewRentalsController(RentalRepository rentalRepository, 
            CustomerRepository customerRepository, BookRepository bookRepository)
        {
            this.rentalRepository = rentalRepository;
            this.customerRepository = customerRepository;
            this.bookRepository = bookRepository;
        }

        [HttpPost]
        public IActionResult CreateNewRental([FromBody] NewRentalDto newRental)
        {
            var customer = customerRepository.GetOne(newRental.CustomerId);

            var books = bookRepository.GetAll()
                .Where(b => newRental.BookIds.Contains(b.Id.Value)).ToList();

            foreach (var book in books)
            {
                if (book.NumberAvailable == 0)
                    return BadRequest("Book is not available");

                book.NumberAvailable--;
                var rental = new Rental()
                {
                    Customer = customer,
                    Book = book,
                    DateRented = DateTime.Now
                };

                rentalRepository.Add(rental);
            }

            rentalRepository.Save();

            return Ok();
        }
    }
}
