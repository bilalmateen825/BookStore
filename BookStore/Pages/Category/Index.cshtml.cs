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
    public class IndexModel : PageModel
    {
        private readonly BookStore.DataLayer.BookDBContext _context;

        public IndexModel(BookStore.DataLayer.BookDBContext context)
        {
            _context = context;
        }

        public IList<BookCategory> BookCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BooksCategories != null)
            {
                BookCategory = await _context.BooksCategories.ToListAsync();
            }
        }
    }
}
