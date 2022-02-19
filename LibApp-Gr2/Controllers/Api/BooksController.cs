using AutoMapper;
using LibApp.Dtos;
using LibApp.Models;
using LibApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibApp.Controllers.Api
{
    // kontroler przetwarzający informacje o książkach (API)
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookRepository bookRepository;
        private readonly IMapper mapper;

        public BooksController(IMapper mapper, BookRepository bookRepository)
        {
            this.mapper = mapper;
            this.bookRepository = bookRepository;
        }

        [HttpGet]
        public IActionResult GetAll(string query = null)
        {
            var books = bookRepository.GetAll()
                .Where(b => query == null 
                || b.Name.ToUpper().Contains(query.ToUpper())
                || b.AuthorName.ToUpper().Contains(query.ToUpper()))
                .Where(b => query == null || b.NumberAvailable > 0)
                .Select(mapper.Map<Book, BookDto>);

            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Book book = bookRepository.GetOne(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BookDto>(book));
        }

        [HttpPost]
        public IActionResult Add(BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var book = mapper.Map<Book>(bookDto);
            bookRepository.Add(book);
            bookRepository.Save();
            bookDto.Id = (int)book.Id;

            return CreatedAtRoute(new { id = bookDto.Id }, bookDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var book = bookRepository.GetOne(id);

            if (book == null)
            {
                return NotFound();
            }

            book.Id = (int)bookDto.Id;
            book.DateAdded = bookDto.DateAdded;
            book.GenreId = bookDto.GenreId;
            book.ReleaseDate = bookDto.ReleaseDate;
            book.DateAdded = bookDto.DateAdded;
            book.NumberAvailable = bookDto.NumberAvailable;
            book.NumberInStock = bookDto.NumberInStock;
            book.AuthorName = bookDto.AuthorName;
            book.Name = bookDto.Name;
            bookRepository.Save();

            return Ok(bookDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = bookRepository.GetOne(id);
            
            if (book == null)
            {
                return NotFound();
            }

            bookRepository.Delete((int)book.Id);
            bookRepository.Save();

            return NoContent();
        }
    }
}
