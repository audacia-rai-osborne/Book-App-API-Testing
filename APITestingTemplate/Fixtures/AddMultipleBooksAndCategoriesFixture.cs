using System;
using System.Collections.Generic;
using System.Linq;
using APITestingTemplate.Helpers;
using APITestingTemplate.Models.CombinedDtos;
using APITestingTemplate.Models.Dtos;
using Audacia.Testing.Api;

namespace APITestingTemplate.Fixtures
{
    public class AddMultipleBooksAndCategoriesFixture : ApiTestsBase, IDisposable
    {
        public AddBookandCategoryData BookData { get; }

        public AddMultipleBooksAndCategoriesFixture()
        {
            using var bookHelper = new BookHelper();

            BookData = bookHelper.AddFiveBooksandCategories();

        }

        public void Dispose()
        {
            using var bookHelper = new BookHelper();

            // delete all books and then their book category
            bookHelper.DeleteBookandCategory(BookData.BookCategoryData.ElementAt(0).Id, BookData.BookData.ElementAt(0).Id);
            bookHelper.DeleteBookandCategory(BookData.BookCategoryData.ElementAt(1).Id, BookData.BookData.ElementAt(1).Id);
            bookHelper.DeleteBookandCategory(BookData.BookCategoryData.ElementAt(2).Id,BookData.BookData.ElementAt(2).Id);
            bookHelper.DeleteBookandCategory(BookData.BookCategoryData.ElementAt(3).Id, BookData.BookData.ElementAt(3).Id);
            bookHelper.DeleteBookandCategory(BookData.BookCategoryData.ElementAt(4).Id, BookData.BookData.ElementAt(4).Id);


        }
    }
}