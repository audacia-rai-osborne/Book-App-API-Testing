using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using APITestingTemplate.Fixtures;
using APITestingTemplate.Helpers;
using APITestingTemplate.Models.Dtos;
using Audacia.Testing.Api;
using FluentAssertions;
using Xunit;

namespace APITestingTemplate.Tests.Book
{
    public class UpdateABookTest : ApiTestsBase, IClassFixture<AddBookFixture>
    {

        private readonly AddBookFixture _addBookFixture;

        public UpdateABookTest(AddBookFixture addBookFixture)
        {
            _addBookFixture = addBookFixture;
        }

        [Fact]
        public void Scenario_8_As_a_user_I_can_update_a_book()
        {
            // Get book details
            var bookDetails = _addBookFixture.BookData.BookData;
            var categoryDetails = _addBookFixture.BookData.BookCategoryData;

            //Set Id you want to get
            var getBookId = bookDetails.First().Id;
            var bookCategoryId = categoryDetails.First().Id;

            // set up edit book request
            var editBookRequest = SetupWithoutSave<UpdateBookRequest>();
            editBookRequest.Id = getBookId;
            editBookRequest.Title = "New Title";
            editBookRequest.Description = "New description";
            editBookRequest.Author = "New Author Name";
            editBookRequest.PublishedYear = 2001;
            editBookRequest.AvailableFrom = DateTimeOffset.Parse("2022 - 09 - 23T13: 17:10.728Z");
            editBookRequest.HasEBook = false;
            editBookRequest.BookCategoryId = bookCategoryId;

            // Call the 
            var editBookResponse = Put<GetBookDtoCommandResult>(editBookRequest, Resources.UpdateBook, null);

            // Check the status code is ok
            editBookResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // Check that the book details are correct
            editBookResponse.Data.Output.Title.Should().Be(editBookRequest.Title);
            editBookResponse.Data.Output.Author.Should().Be(editBookRequest.Author);
            editBookResponse.Data.Output.PublishedYear.Should().Be(editBookRequest.PublishedYear);

        }

        [Fact]
        public void Scenario_9_As_a_user_I_cannot_update_a_book_when_using_the_form_wrong()
        {
            // set up edit book request
            var editBookIncorrectlyRequest = SetupWithoutSave<UpdateBookRequest>();
            editBookIncorrectlyRequest.Id = 470;
            editBookIncorrectlyRequest.Title = "";

            // Call the UPDATE API
            var editBookResponse = Put<GetBookDtoCommandResult>(editBookIncorrectlyRequest, Resources.UpdateBook, null);

            // Check the status code is ok
            editBookResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            // Check that the book details are correct
            //editBookResponse.Data.Should().Contain(b => b.Title == "ApiTestTitle");

        }
    }
}