using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebAppActions.Models.Services;
using WebAppActions.Models.ViewModels;

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
            var bookViewModels = await _bookService.GetAllBooksAsync();
            return View(bookViewModels);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookViewModel = await _bookService.GetBookDetailsAsync(id.Value);

            if (bookViewModel == null)
            {
                return NotFound();
            }

            return View(bookViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = await _bookService.GetBookFormDefaultsAsync();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Author,CategoryId")] BookFormViewModel bookFormViewModel)
        {
            if (ModelState.IsValid)
            {
                await _bookService.CreateBookAsync(bookFormViewModel);
                return RedirectToAction(nameof(Index));
            }
            
            bookFormViewModel.CategoryList = await _bookService.GetCategorySelectListAsync(bookFormViewModel.CategoryId);
            return View(bookFormViewModel);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookFormViewModel = await _bookService.GetBookForEditAsync(id.Value);
            if (bookFormViewModel == null)
            {
                return NotFound();
            }
            return View(bookFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Author,CategoryId,CreatedAt")] BookFormViewModel bookFormViewModel)
        {
            if (id != bookFormViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.UpdateBookAsync(bookFormViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bookService.BookExistsAsync(bookFormViewModel.Id))
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
            
            bookFormViewModel.CategoryList = await _bookService.GetCategorySelectListAsync(bookFormViewModel.CategoryId);
            return View(bookFormViewModel);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookViewModel = await _bookService.GetBookDetailsAsync(id.Value);
            if (bookViewModel == null)
            {
                return NotFound();
            }

            return View(bookViewModel);
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