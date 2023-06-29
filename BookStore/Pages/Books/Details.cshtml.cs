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
    public class DetailsModel : PageModel
    {
        private readonly BookStore.DataLayer.BookDBContext _context;

        public DetailsModel(BookStore.DataLayer.BookDBContext context)
        {
            _context = context;
        }

      public BookEntity BookEntity { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BooksCollections == null)
            {
                return NotFound();
            }

            var bookentity = await _context.BooksCollections.FirstOrDefaultAsync(m => m.BookId == id);
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
    }
}
