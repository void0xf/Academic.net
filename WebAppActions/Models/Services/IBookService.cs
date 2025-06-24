using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppActions.Models;
using WebAppActions.Models.ViewModels;

namespace WebAppActions.Models.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetAllBooksAsync();
        Task<BookFormViewModel> GetBookForEditAsync(Guid id);
        Task<BookDetailsViewModel> GetBookDetailsAsync(Guid id);
        Task<SelectList> GetCategorySelectListAsync(Guid? selectedId = null);
        Task<BookViewModel> CreateBookAsync(BookFormViewModel bookFormViewModel);
        Task UpdateBookAsync(BookFormViewModel bookFormViewModel);
        Task DeleteBookAsync(Guid id);
        Task<bool> BookExistsAsync(Guid id);
        Task<BookFormViewModel> GetBookFormDefaultsAsync();
    }
} 