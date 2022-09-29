using System;
using System.Linq;
using APITestingTemplate.Helpers;
using APITestingTemplate.Models.CombinedDtos;
using APITestingTemplate.Models.Dtos;
using Audacia.Testing.Api;

namespace APITestingTemplate.Fixtures
{
    public class AddBookFixture : ApiTestsBase, IDisposable

    {
        public AddBookandCategoryData BookData { get; }

        public AddBookFixture()
        {
            using var bookHelper = new BookHelper();
            
            BookData = bookHelper.AddBookWithBookCategory();
            

        }

        public void Dispose()
        {
            using var bookHelper = new BookHelper();

            bookHelper.DeleteBookandCategory(BookData.BookCategoryData.First().Id, BookData.BookData.First().Id);
        }
    }
}

