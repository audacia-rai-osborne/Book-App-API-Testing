using System;
using System.Collections.Generic;
using System.Linq;
using APITestingTemplate.Helpers;
using APITestingTemplate.Models.CombinedDtos;
using APITestingTemplate.Models.Dtos;
using Audacia.Testing.Api;

namespace APITestingTemplate.Fixtures
{
    public class AddMultipleCategoriesFixture : ApiTestsBase, IDisposable
    {
        public AddCategoryData BookCategoryData { get; }

        public AddMultipleCategoriesFixture()
        {
            using var categoriesHelper = new BookCategoryHelper();

            BookCategoryData = categoriesHelper.AddFiveBookCategories();

        }

        public void Dispose()
        {
            using var categoriesHelper = new BookCategoryHelper();

            // delete the book categories
         
            categoriesHelper.DeleteBookCategory(BookCategoryData.BookCategoryData.First().Id);
            categoriesHelper.DeleteBookCategory(BookCategoryData.BookCategoryData.ElementAt(1).Id);
            categoriesHelper.DeleteBookCategory(BookCategoryData.BookCategoryData.ElementAt(2).Id);
            categoriesHelper.DeleteBookCategory(BookCategoryData.BookCategoryData.ElementAt(3).Id);
            categoriesHelper.DeleteBookCategory(BookCategoryData.BookCategoryData.ElementAt(4).Id);


        }
    }
}