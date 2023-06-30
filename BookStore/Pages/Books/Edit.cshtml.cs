using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.DataLayer;
using BookStore.Entity;

namespace BookStore.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly BookStore.DataLayer.BookDBContext _context;
        public List<SelectListItem> BookCategoryItemSource { get; set; }

        public EditModel(BookStore.DataLayer.BookDBContext context)
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

            BookCategoryItemSource = _context.BooksCategories.Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.ID.ToString()
            }).ToList();

            var bookentity =  await _context.BooksCollections.FirstOrDefaultAsync(m => m.BookId == id);
            
            if (bookentity == null)
            {
                return NotFound();
            }

            BookEntity = bookentity;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BookEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookEntityExists(BookEntity.BookId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookEntityExists(int id)
        {
          return (_context.BooksCollections?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
