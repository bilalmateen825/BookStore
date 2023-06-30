using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.DataLayer;
using BookStore.Entity;

namespace BookStore.Pages.Category
{
    public class DeleteModel : PageModel
    {
        private readonly BookStore.DataLayer.BookDBContext _context;

        public DeleteModel(BookStore.DataLayer.BookDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public BookCategory BookCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BooksCategories == null)
            {
                return NotFound();
            }

            var bookcategory = await _context.BooksCategories.FirstOrDefaultAsync(m => m.ID == id);

            if (bookcategory == null)
            {
                return NotFound();
            }
            else 
            {
                BookCategory = bookcategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.BooksCategories == null)
            {
                return NotFound();
            }
            var bookcategory = await _context.BooksCategories.FindAsync(id);

            if (bookcategory != null)
            {
                BookCategory = bookcategory;
                _context.BooksCategories.Remove(BookCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
