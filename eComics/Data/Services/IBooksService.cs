﻿using eComics.Data.Base;
using eComics.Data.ViewModels;
using eComics.Models;

namespace eComics.Data.Services
{
    public interface IBooksService 
    {
        Task<Book> GetByIdAsync(int id);
        Task<NewBookDropdownsVM> GetNewBookDropdownsValues();

        Task AddNewBookAsync(NewBookVM data);

        Task UpdateBookAsync(NewBookVM data);

        Task<IEnumerable<Book>> GetBooksWithPublishersAsync();
    }
}
