using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppActions.Models.Repositories;
using WebAppActions.Models.ViewModels;

namespace WebAppActions.Models.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        private BookViewModel MapBookToViewModel(Book book)
        {
            if (book == null) return null;
            return new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                CategoryName = book.Category?.Name,
                CreatedAt = book.CreatedAt
            };
        }

        private BookFormViewModel MapBookToFormViewModel(Book book)
        {
            if (book == null) return null;
            return new BookFormViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                CategoryId = book.CategoryId,
                CreatedAt = book.CreatedAt
            };
        }

        private BookDetailsViewModel MapBookToDetailsViewModel(Book book)
        {
            if (book == null) return null;
            return new BookDetailsViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                CategoryName = book.Category?.Name,
                CreatedAt = book.CreatedAt,
                Owners = book.UserBooks?.Select(ub => new BookOwnerViewModel { Username = ub.User?.Username }).ToList() ?? new List<BookOwnerViewModel>(),
                Reviews = book.Reviews?.Select(r => new ReviewViewModel { Rating = r.Rating, Comment = r.Comment, CreatedAt = r.CreatedAt }).ToList() ?? new List<ReviewViewModel>()
            };
        }

        public async Task<IEnumerable<BookViewModel>> GetAllBooksAsync()
        {
            var books = await _repository.GetAllBooksAsync();
            return books.Select(b => new BookViewModel 
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                CategoryName = b.Category?.Name,
                CreatedAt = b.CreatedAt
            }).ToList();
        }

        public async Task<BookFormViewModel> GetBookForEditAsync(Guid id)
        {
            var book = await _repository.GetBookByIdAsync(id);
            if (book == null) return null;

            var viewModel = MapBookToFormViewModel(book);
            viewModel.CategoryList = await GetCategorySelectListAsync(book.CategoryId);
            return viewModel;
        }

        public async Task<BookDetailsViewModel> GetBookDetailsAsync(Guid id)
        {
            var book = await _repository.GetBookWithDetailsAsync(id);
            return MapBookToDetailsViewModel(book);
        }

        public async Task<SelectList> GetCategorySelectListAsync(Guid? selectedId = null)
        {
            return await _repository.GetCategorySelectListAsync(selectedId);
        }

        public async Task<BookViewModel> CreateBookAsync(BookFormViewModel bookFormViewModel)
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = bookFormViewModel.Title,
                Author = bookFormViewModel.Author,
                CategoryId = bookFormViewModel.CategoryId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.CreateBookAsync(book);
            return MapBookToViewModel(book);
        }

        public async Task UpdateBookAsync(BookFormViewModel bookFormViewModel)
        {
            var book = await _repository.GetBookByIdAsync(bookFormViewModel.Id);
            if (book != null)
            {
                book.Title = bookFormViewModel.Title;
                book.Author = bookFormViewModel.Author;
                book.CategoryId = bookFormViewModel.CategoryId;

                await _repository.UpdateBookAsync(book);
            }
        }

        public async Task DeleteBookAsync(Guid id)
        {
            var book = await _repository.GetBookByIdAsync(id);
            if (book != null)
            {
                await _repository.DeleteBookAsync(book);
            }
        }

        public async Task<bool> BookExistsAsync(Guid id)
        {
            return await _repository.BookExistsAsync(id);
        }

        public async Task<BookFormViewModel> GetBookFormDefaultsAsync()
        {
            return new BookFormViewModel
            {
                CategoryList = await GetCategorySelectListAsync()
            };
        }
    }
} 