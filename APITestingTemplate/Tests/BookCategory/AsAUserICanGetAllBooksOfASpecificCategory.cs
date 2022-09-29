using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using APITestingTemplate.Fixtures;
using APITestingTemplate.Helpers;
using APITestingTemplate.Models.Dtos;
using Audacia.Testing.Api;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace APITestingTemplate.Tests.BookCategory
{
    public class GetAllBooksOfASpecificCategoryTest : ApiTestsBase, IClassFixture<AddMultipleBooksFixture>
    {
        private readonly AddMultipleBooksFixture _addMultipleBooksFixture;

        private readonly BookHelper _bookHelper;


        public GetAllBooksOfASpecificCategoryTest(AddMultipleBooksFixture addMultipleBooksFixture)
        {
            // _addCategoryFixture = addCategoryFixture;

            _addMultipleBooksFixture = addMultipleBooksFixture;
            _bookHelper = new BookHelper();

        }

        [Fact]
        public void Scenario_13_As_a_user_I_can_get_all_books_of_a_specific__category()
        {
            // get category data
            var getCategoryId = _addMultipleBooksFixture.BookData.BookCategoryData.First().Id;
            var getBookTitleOne = _addMultipleBooksFixture.BookData.BookData.First().Title;
            var getBookTitleTwo = _addMultipleBooksFixture.BookData.BookData.ElementAt(1).Title;
            var getBookAuthorOne = _addMultipleBooksFixture.BookData.BookData.First().Author;
            var getBookAuthorTwo = _addMultipleBooksFixture.BookData.BookData.ElementAt(1).Author;
            var getBookPublishedYearOne = _addMultipleBooksFixture.BookData.BookData.First().PublishedYear;
            var getBookPublishedYearTwo = _addMultipleBooksFixture.BookData.BookData.ElementAt(1).PublishedYear;

            // Define category you want to search

            string categoryId = Convert.ToString(getCategoryId);

            // Call the get all books from a specific category
           var getAllBooksOfASpecificCategoryResponse = GetAll<GetBookDtoIEnumerableCommandResult>(Resources.GetAllBooksOfSpecificCategory(categoryId), null);

            // Check the status code is ok
           getAllBooksOfASpecificCategoryResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            //Convert response data
            // var addedData = JsonConvert.DeserializeObject<GetBookDtoIEnumerableCommandResult>(getAllBooksOfASpecificCategoryResponse.Content);
            //check a few book titles that should be here 
            //addedData.Output.Should().Contain(a => a.Title == "h" || a.Title == "Another book");
            getAllBooksOfASpecificCategoryResponse.Data.Output.Should().Contain(a => a.Title == getBookTitleOne || a.Title == getBookTitleTwo);
            getAllBooksOfASpecificCategoryResponse.Data.Output.Should().Contain(a => a.Author == getBookAuthorOne || a.Author == getBookAuthorTwo);
            getAllBooksOfASpecificCategoryResponse.Data.Output.Should().Contain(a => a.PublishedYear == getBookPublishedYearOne || a.PublishedYear == getBookPublishedYearTwo);
            getAllBooksOfASpecificCategoryResponse.Data.Output.Should().Contain(a => a.BookCategoryId == getCategoryId);
        }
    }
};