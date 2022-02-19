using System;
using System.ComponentModel.DataAnnotations;

namespace LibApp.Models
{
    public class Book
    {
		[Key]
        public int? Id { get; set; }
		[Required]
		[StringLength(255)]
		public string Name { get; set; }
		[Required]
		public string AuthorName { get; set; }
		public Genre Genre { get; set; }
		[Required]
		public byte GenreId { get; set; }
		public DateTime DateAdded { get; set; }
		[Required]
		public DateTime ReleaseDate { get; set; }
		[Required]
		[Range(1, 20)]
		public int NumberInStock { get; set; }
		public int NumberAvailable { get; set; }
	}
      
}
