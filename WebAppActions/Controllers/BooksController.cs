using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebAppActions.Models;
using WebAppActions.Models.Services;

namespace WebAppActions.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBookWithDetailsAsync(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryList = await _bookService.GetCategorySelectListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Author,CategoryId")] Book book)
        {
            ModelState.Remove("Reviews");
            ModelState.Remove("Category");
            ModelState.Remove("UserBooks");
            
            if (ModelState.IsValid)
            {
                await _bookService.CreateBookAsync(book);
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.CategoryList = await _bookService.GetCategorySelectListAsync(book.CategoryId);
            return View(book);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBookByIdAsync(id.Value);
            if (book == null)
            {
                return NotFound();
            }
            
            ViewBag.CategoryList = await _bookService.GetCategorySelectListAsync(book.CategoryId);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Author,CategoryId,CreatedAt")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Reviews");
            ModelState.Remove("Category");
            ModelState.Remove("UserBooks");
            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.UpdateBookAsync(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bookService.BookExistsAsync(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.CategoryList = await _bookService.GetCategorySelectListAsync(book.CategoryId);
            return View(book);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookService.GetBookWithDetailsAsync(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
} 