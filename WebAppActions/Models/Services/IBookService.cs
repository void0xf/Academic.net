using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppActions.Models;

namespace WebAppActions.Models.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(Guid id);
        Task<Book> GetBookWithDetailsAsync(Guid id);
        Task<SelectList> GetCategorySelectListAsync(Guid? selectedId = null);
        Task<Book> CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(Guid id);
        Task<bool> BookExistsAsync(Guid id);
    }
} 