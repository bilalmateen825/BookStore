using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.DataLayer;
using BookStore.Entity;
using BookStore.Enums;
using BookStore;
using BookStore.Classes.Sorting;
using BookStore.Classes.Data;
using Microsoft.Extensions.Caching.Memory;

namespace BookStore.Pages.Books
{

    public class IndexModel : PageModel
    {
        private readonly BookStore.DataLayer.BookDBContext _context;
        private readonly IMemoryCache _cache;

        public CustomDictionary<string, SortInfo> DictSortInfo { get; set; }

        public IndexModel(BookStore.DataLayer.BookDBContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _cache = memoryCache;
        }

        public IList<BookEntity> BookEntity { get; set; } = default!;

        public async Task OnGetAsync(string sortAttribute, string filterString)
        {
            SortInfo sortInfo = null;
            ENBookAttributes enBookAttributes = ENBookAttributes.None;

            // Retrieve DictSortInfo from cache
            DictSortInfo = _cache.Get<CustomDictionary<string, SortInfo>>("DictSortInfo");

            if (DictSortInfo == null)
            {
                DictSortInfo = new CustomDictionary<string, SortInfo>();
                _cache.Set("DictSortInfo", DictSortInfo);
            }

            if (sortAttribute != null)
            {
                if (Enum.TryParse(sortAttribute, out enBookAttributes))
                {
                    sortInfo = DictSortInfo.FindByKey(sortAttribute);

                    if (sortInfo == null)
                    {
                        sortInfo = new SortInfo()
                        {
                            Attribute = enBookAttributes,
                        };

                        DictSortInfo.Add(sortAttribute, sortInfo);
                    }
                    else
                        sortInfo.SortDecision();
                }
            }

            if (_context.BooksCollections != null)
            {
                if (!string.IsNullOrEmpty(filterString))
                    BookEntity = await _context.BooksCollections.Include(x => x.BookCategory).
                            Where(
                                    x => x.Title.Contains(filterString) ||
                                    x.AuthorName.Contains(filterString) ||
                                    x.BookCategory.CategoryName.Contains(filterString) ||
                                    x.Rating.Contains(filterString)
                                    )
                            .ToListAsync();
                else
                    BookEntity = await _context.BooksCollections.Include(x => x.BookCategory).ToListAsync();
            }

            switch (enBookAttributes)
            {
                case ENBookAttributes.Title:
                    {
                        //Sorting List internally
                        if (sortInfo.SortType == ENSortingType.Ascending)
                            ((List<BookEntity>)BookEntity).Sort((x, y) => string.Compare(x.Title, y.Title));
                        else
                            ((List<BookEntity>)BookEntity).Sort((x, y) => string.Compare(y.Title, x.Title));

                        break;
                    }
                case ENBookAttributes.Author:
                    {
                        //Make Clone List
                        if (sortInfo.SortType == ENSortingType.Ascending)
                            BookEntity = BookEntity.OrderBy(x => x.AuthorName).ToList();
                        else
                            BookEntity = BookEntity.OrderByDescending(x => x.AuthorName).ToList();

                        break;
                    }
                case ENBookAttributes.ReleaseDate:
                    {
                        if (sortInfo.SortType == ENSortingType.Ascending)
                            BookEntity = BookEntity.OrderBy(x => x.ReleaseDate).ToList();
                        else
                            BookEntity = BookEntity.OrderByDescending(x => x.ReleaseDate).ToList();

                        break;
                    }
                case ENBookAttributes.Rating:
                    {
                        if (sortInfo.SortType == ENSortingType.Ascending)
                            BookEntity = BookEntity.OrderBy(x => x.Rating).ToList();
                        else
                            BookEntity = BookEntity.OrderByDescending(x => x.Rating).ToList();

                        /*
                         if (sortInfo.SortType == ENSortingType.Ascending)
                            ((List<BookEntity>)BookEntity).Sort((x, y) => x.CompareTo(y));
                         else
                            ((List<BookEntity>)BookEntity).Sort((x, y) => y.CompareTo(x));
                         */

                        break;
                    }
                case ENBookAttributes.Category:
                    {
                        if (sortInfo.SortType == ENSortingType.Ascending)
                            BookEntity = BookEntity.OrderBy(x => x.BookCategory.CategoryName).ToList();
                        else
                            BookEntity = BookEntity.OrderByDescending(x => x.BookCategory.CategoryName).ToList();

                        break;
                    }
                case ENBookAttributes.None:
                    {
                        if (DictSortInfo != null)
                        {
                            sortInfo = DictSortInfo.FindByKey(ENBookAttributes.Title.ToString());

                            if (sortInfo == null)
                            {
                                sortInfo = new SortInfo()
                                {
                                    Attribute = enBookAttributes,
                                };

                                DictSortInfo.Add(ENBookAttributes.Title.ToString(), sortInfo);
                            }
                        }

                        //Sorting List internally
                        if (sortInfo.SortType == ENSortingType.Ascending)
                            ((List<BookEntity>)BookEntity).Sort((x, y) => string.Compare(x.Title, y.Title));
                        else
                            ((List<BookEntity>)BookEntity).Sort((x, y) => string.Compare(y.Title, x.Title));

                        break;
                    }
            }
        }
    }
}
