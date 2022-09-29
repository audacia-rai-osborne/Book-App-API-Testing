using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using APITestingTemplate.Fixtures;
using APITestingTemplate.Helpers;
using APITestingTemplate.Models.CombinedDtos;
using APITestingTemplate.Models.Dtos;
using Audacia.Extensions;
using Audacia.Testing.Api;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace APITestingTemplate.Tests.Book
{
    public class GetAllBooksTest : ApiTestsBase, IClassFixture<AddMultipleBooksAndCategoriesFixture>
    {
        //private readonly AddMultipleBooksFixture _addMultipleBooksFixture;

        //private readonly BookHelper _bookHelper;


        //public GetAllBooksTest(AddMultipleBooksFixture addMultipleBooksFixture)
        //{
        //    // _addCategoryFixture = addCategoryFixture;

        //    _addMultipleBooksFixture = addMultipleBooksFixture;
        //    _bookHelper = new BookHelper();

        //}

        private Random Random { get; } = new();

        private readonly AddMultipleBooksAndCategoriesFixture _addMultipleBooksAndCategoriesFixture;

        private readonly BookHelper _bookHelper;

        public GetAllBooksTest(AddMultipleBooksAndCategoriesFixture addMultipleBooksAndCategoriesFixture)
        {
            _addMultipleBooksAndCategoriesFixture = addMultipleBooksAndCategoriesFixture;
            _bookHelper = new BookHelper();
        }

        [Trait("Category", "Get All")]
        [Trait("Category", "Happy Path")]
        [Fact]
        public void Scenario_2_As_a_user_I_can_get_all_books()
        {
            //Get book details
            var getCategoryIdOne = _addMultipleBooksAndCategoriesFixture.BookData.BookCategoryData.First().Id;
            var getCategoryIdTwo = _addMultipleBooksAndCategoriesFixture.BookData.BookCategoryData.ElementAt(3).Id;
            var getBookTitleOne = _addMultipleBooksAndCategoriesFixture.BookData.BookData.First().Title;
            var getBookTitleTwo = _addMultipleBooksAndCategoriesFixture.BookData.BookData.ElementAt(3).Title;
            var getBookAuthorOne = _addMultipleBooksAndCategoriesFixture.BookData.BookData.First().Author;
            var getBookAuthorTwo = _addMultipleBooksAndCategoriesFixture.BookData.BookData.ElementAt(3).Author;
            var getBookPublishedYearOne = _addMultipleBooksAndCategoriesFixture.BookData.BookData.First().PublishedYear;
            var getBookPublishedYearTwo = _addMultipleBooksAndCategoriesFixture.BookData.BookData.ElementAt(3).PublishedYear;


            // Call the get all books
            var getAllBooksResponse = GetAll<List<GetBookDto>>(Resources.GetAllBooks, null);

            // Check the status code is ok
            getAllBooksResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // Check that the book details are correct
            getAllBooksResponse.Data.Should().Contain(a => a.Title == getBookTitleOne || a.Title == getBookTitleTwo);
            getAllBooksResponse.Data.Should().Contain(a => a.PublishedYear == getBookPublishedYearOne || a.PublishedYear == getBookPublishedYearTwo);
            getAllBooksResponse.Data.Should().Contain(a => a.Author == getBookAuthorOne || a.Author == getBookAuthorTwo);
            getAllBooksResponse.Data.Should().Contain(a => a.BookCategoryId == getCategoryIdOne || a.BookCategoryId == getCategoryIdTwo);
        }
    }
}