using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.DataLayer;
using BookStore.Entity;

namespace BookStore.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly BookStore.DataLayer.BookDBContext _context;

        public DeleteModel(BookStore.DataLayer.BookDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public BookEntity BookEntity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BooksCollections == null)
            {
                return NotFound();
            }

            var bookentity = await _context.BooksCollections.Include(x=>x.BookCategory).FirstOrDefaultAsync(m => m.BookId == id);

            if (bookentity == null)
            {
                return NotFound();
            }
            else 
            {
                BookEntity = bookentity;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.BooksCollections == null)
            {
                return NotFound();
            }
            var bookentity = await _context.BooksCollections.FindAsync(id);

            if (bookentity != null)
            {
                BookEntity = bookentity;
                _context.BooksCollections.Remove(BookEntity);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
