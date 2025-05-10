using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppActions.Models.Repositories;

namespace WebAppActions.Models.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _repository.GetAllBooksAsync();
        }

        public async Task<Book> GetBookByIdAsync(Guid id)
        {
            return await _repository.GetBookByIdAsync(id);
        }

        public async Task<Book> GetBookWithDetailsAsync(Guid id)
        {
            return await _repository.GetBookWithDetailsAsync(id);
        }

        public async Task<SelectList> GetCategorySelectListAsync(Guid? selectedId = null)
        {
            return await _repository.GetCategorySelectListAsync(selectedId);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            book.Id = Guid.NewGuid();
            book.CreatedAt = DateTime.UtcNow;
            
            if (book.CategoryId == Guid.Empty)
            {
                book.CategoryId = null;
            }
            
            await _repository.CreateBookAsync(book);
            return book;
        }

        public async Task UpdateBookAsync(Book book)
        {
            if (book.CategoryId == Guid.Empty)
            {
                book.CategoryId = null;
            }
            
            await _repository.UpdateBookAsync(book);
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
    }
} 