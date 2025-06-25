using AutoMapper;
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
        private readonly IMapper _mapper;

        public BookService(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookViewModel>> GetAllBooksAsync()
        {
            var books = await _repository.GetAllBooksAsync();
            return _mapper.Map<IEnumerable<BookViewModel>>(books);
        }

        public async Task<BookFormViewModel> GetBookForEditAsync(Guid id)
        {
            var book = await _repository.GetBookByIdAsync(id);
            if (book == null) return null;

            var viewModel = _mapper.Map<BookFormViewModel>(book);
            viewModel.CategoryList = await GetCategorySelectListAsync(book.CategoryId);
            return viewModel;
        }

        public async Task<BookDetailsViewModel> GetBookDetailsAsync(Guid id)
        {
            var book = await _repository.GetBookWithDetailsAsync(id);
            return _mapper.Map<BookDetailsViewModel>(book);
        }

        public async Task<SelectList> GetCategorySelectListAsync(Guid? selectedId = null)
        {
            return await _repository.GetCategorySelectListAsync(selectedId);
        }

        public async Task<BookViewModel> CreateBookAsync(BookFormViewModel bookFormViewModel)
        {
            var book = _mapper.Map<Book>(bookFormViewModel);
            book.Id = Guid.NewGuid();
            book.CreatedAt = System.DateTime.UtcNow;

            await _repository.CreateBookAsync(book);
            return _mapper.Map<BookViewModel>(book);
        }

        public async Task UpdateBookAsync(BookFormViewModel bookFormViewModel)
        {
            var book = await _repository.GetBookByIdAsync(bookFormViewModel.Id);
            if (book != null)
            {
                _mapper.Map(bookFormViewModel, book);
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