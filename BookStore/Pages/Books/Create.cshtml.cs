﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore.DataLayer;
using BookStore.Entity;

namespace BookStore.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly BookStore.DataLayer.BookDBContext _context;

        public List<SelectListItem> BookCategoryItemSource { get; set; }

        public CreateModel(BookStore.DataLayer.BookDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            BookCategoryItemSource = _context.BooksCategories.Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.ID.ToString()
            }).ToList();

            return Page();
        }

        [BindProperty]
        public BookEntity BookEntity { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.BooksCollections == null || BookEntity == null)
            {
                return Page();
            }

            _context.BooksCollections.Add(BookEntity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
