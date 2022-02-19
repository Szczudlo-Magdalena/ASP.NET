using System;
using System.ComponentModel.DataAnnotations;

namespace LibApp.Models
{
    public class Rental
    {
        public int Id { get; set; }
        [Required]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        [Required]
        public Book Book { get; set; }
        public int BookId { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}
