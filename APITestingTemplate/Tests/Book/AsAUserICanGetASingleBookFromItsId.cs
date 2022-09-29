using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using APITestingTemplate.Fixtures;
using APITestingTemplate.Helpers;
using APITestingTemplate.Models.Dtos;
using Audacia.Testing.Api;
using FluentAssertions;
using FluentAssertions.Primitives;
using Xunit;

namespace APITestingTemplate.Tests.Book
{
    public class GetBooksTests : ApiTestsBase, IClassFixture<AddBookFixture>
    {

        private readonly AddBookFixture _addBookFixture;

        public GetBooksTests(AddBookFixture addBookFixture)
        {
            _addBookFixture = addBookFixture;
        }


        [Fact]
        public void Scenario_3_As_a_user_I_can_get_a_book_by_its_Id()
        {

            // Get book details
            var bookDetails = _addBookFixture.BookData.BookData;
            var categoryDetails = _addBookFixture.BookData.BookCategoryData;

            //Set Id you want to get
            var getBookId = bookDetails.First().Id;
            var bookCategoryId = categoryDetails.First().Id;

            // Call the get API to get the book by its ID
            var getBookResponse = Get<GetBookDtoCommandResult>(getBookId, Resources.GetBookById);

            // Check the status code is ok
            getBookResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // Check that the book details are correct
            getBookResponse.Data.Output.Title.Should().Be(bookDetails.First().Title);
            getBookResponse.Data.Output.Author.Should().Be(bookDetails.First().Author);
            getBookResponse.Data.Output.PublishedYear.Should().Be(bookDetails.First().PublishedYear);

        }

        [Fact]
        public void Scenario_4_As_a_user_I_cannot_get_a_book_by_an_Id_that_doesnt_exist()
        {

            // Set the bookId you wish to get
            var getBookId = 1000;

            // Call the get API to get the book by its ID
            var getBookResponse = Get<GetBookDtoCommandResult>(getBookId, Resources.GetBookById);

            // Check the status code is ok
            getBookResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }
    }
}
