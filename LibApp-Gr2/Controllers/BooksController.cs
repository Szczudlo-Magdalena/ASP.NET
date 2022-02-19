using LibApp.Data;
using LibApp.Dtos;
using LibApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibApp.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly RestApiClient api;

        public BooksController(RestApiClient api)
        {
            this.api = api;
        }

        public async Task<IActionResult> Index()
        {
            List<BookDto> books = await api.GetAllBooks();

            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await api.GetOneBook(id));
        }

        [Authorize(Roles = "StoreManager,Owner")]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await api.GetOneBook(id);
            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookFormViewModel
            {
                Book = book,
                Genres = await api.GetAllGenres()
            };

            return View("BookForm", viewModel);
        }

        [Authorize(Roles = "StoreManager,Owner")]
        public async Task<IActionResult> New()
        {
            var viewModel = new BookFormViewModel
            {
                Genres = await api.GetAllGenres()
            };

            return View("BookForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "StoreManager,Owner")]
        public async Task<IActionResult> Save(BookDto book)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("New", "Books");
            }

            if ((book.Id ?? 0) == 0)
            {
                book.DateAdded = DateTime.Now;
                await api.AddBook(book);
            }
            else
            {
                await api.UpdateBook((int)book.Id, book);
            }

            return RedirectToAction("Index", "Books");
        }
    }
}
