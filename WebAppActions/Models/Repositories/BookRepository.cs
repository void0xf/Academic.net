using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppActions.Models.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.UserBooks)
                    .ThenInclude(ub => ub.User)
                .ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(Guid id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> GetBookWithDetailsAsync(Guid id)
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.UserBooks)
                    .ThenInclude(ub => ub.User)
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<SelectList> GetCategorySelectListAsync(Guid? selectedId = null)
        {
            var categories = await _context.Categories
                .Select(c => new 
                { 
                    Id = c.Id, 
                    Name = c.Name 
                })
                .ToListAsync();
            
            return new SelectList(categories, "Id", "Name", selectedId);
        }

        public async Task CreateBookAsync(Book book)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> BookExistsAsync(Guid id)
        {
            return await _context.Books.AnyAsync(e => e.Id == id);
        }
    }
} 