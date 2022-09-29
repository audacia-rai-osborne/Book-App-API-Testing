using System;
using System.Collections.Generic;
using System.Linq;
using APITestingTemplate.Helpers;
using APITestingTemplate.Models.CombinedDtos;
using APITestingTemplate.Models.Dtos;
using Audacia.Testing.Api;

namespace APITestingTemplate.Fixtures
{
    public class AddMultipleBooksFixture : ApiTestsBase, IDisposable
    {
        public AddBookandCategoryData BookData { get; }

        public AddMultipleBooksFixture()
        {
                using var bookHelper = new BookHelper();

                BookData = bookHelper.AddFiveBooks();

        }

        public void Dispose()
        {
            using var bookHelper = new BookHelper();

           // delete all books and then their book category
                bookHelper.DeleteBook(BookData.BookData.ElementAt(0).Id);
                bookHelper.DeleteBook(BookData.BookData.ElementAt(1).Id);
                bookHelper.DeleteBook(BookData.BookData.ElementAt(2).Id);
                bookHelper.DeleteBook(BookData.BookData.ElementAt(3).Id);
                bookHelper.DeleteBookandCategory(BookData.BookCategoryData.First().Id, BookData.BookData.ElementAt(4).Id);


        }
    }
}