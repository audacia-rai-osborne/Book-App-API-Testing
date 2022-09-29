using System;
using System.Linq;
using APITestingTemplate.Helpers;
using APITestingTemplate.Models.CombinedDtos;
using APITestingTemplate.Models.Dtos;
using Audacia.Testing.Api;

namespace APITestingTemplate.Fixtures
{
    public class AddCategoryFixture : ApiTestsBase, IDisposable
    {
        public GetBookCategoryDto BookCategoryData { get; }

        public GetBookDto BookData { get; }

        public AddCategoryFixture()
        {
            using var bookCategoryHelper = new BookCategoryHelper();

            BookCategoryData = bookCategoryHelper.AddBookCategory();
        }

        public void Dispose()
        {
            using var bookCategoryHelper = new BookCategoryHelper();

            bookCategoryHelper.DeleteBookCategory(BookCategoryData.Id);

            //using var bookHelper = new BookHelper();

            //bookHelper.DeleteBook(BookData.Id);

        }
    }
}