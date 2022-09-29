using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using APITestingTemplate.Fixtures;
using APITestingTemplate.Helpers;
using APITestingTemplate.Models.Dtos;
using Audacia.Random.Extensions;
using Audacia.Testing.Api;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Xunit;

namespace APITestingTemplate.Tests.Book
{
    public class AddABookTest : ApiTestsBase, IClassFixture<AddCategoryFixture>
    {
        private Random Random { get; } = new();

        private readonly AddCategoryFixture _addCategoryFixture;

        private readonly BookHelper _bookHelper;


        public AddABookTest(AddCategoryFixture addCategoryFixture)
        {
            _addCategoryFixture = addCategoryFixture;

            _bookHelper = new BookHelper();
            
        }

        [Fact]
        public void Scenario_5_As_a_user_I_can_add_a_book()
        {

            // Set up book
            var addBook = SetupWithoutSave<AddBookRequest>();

            // Get category details
            var categoryDetails = _addCategoryFixture.BookCategoryData;

            //Set book you want to add
            addBook.BookCategoryId = categoryDetails.Id;
            addBook.Title = Random.Words(2);
            addBook.Author = Random.Forename() + ' ' + Random.Surname();
            addBook.Description = Random.Sentence();
            addBook.PublishedYear =2015;
            addBook.AvailableFrom = DateTime.Parse("2022-09-16T12:55:22.117Z");
            addBook.HasEBook = true;

            // Call POST API to add book
            var addBookResponse = Post<GetBookDtoCommandResult>(addBook, Resources.AddBook, null);

            //Check status response
            addBookResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            //Check response by getting book
            //var addedData = JsonConvert.DeserializeObject<GetBookDtoCommandResult>(addBookResponse.Content);
            //addedData.Output.Title.Should().Be("New Book");
            addBookResponse.Data.Output.Title.Should().Be(addBook.Title);
            addBookResponse.Data.Output.Author.Should().Be(addBook.Author);
            addBookResponse.Data.Output.PublishedYear.Should().Be(addBook.PublishedYear);

            // Delete book
            var bookId = addBookResponse.Data.Output.Id;
            _bookHelper.DeleteBook(bookId);

        }

        [Fact]
        public void Scenario_6_As_a_user_I_cannot_add_a_book_with_an_incorrect_body_request()
        {

            // Set up book
            var addBadBook = SetupWithoutSave<AddBookRequest>();

            // Provide body of POST request
            addBadBook.Title = "Hello";
            // addBadBook.Description = "description";
            addBadBook.Author = "Author Name";
            addBadBook.PublishedYear = -2000;
            addBadBook.AvailableFrom = DateTimeOffset.Parse("2022 - 09 - 23T13: 17:10.728Z");
            addBadBook.HasEBook = true;
            addBadBook.BookCategoryId = 1;

            // Call POST API to add book
            var addBadBookResponse = Post<GetBookDtoCommandResult>(addBadBook, Resources.AddBook, null);

            // Check status response
            if (addBadBookResponse.StatusCode == HttpStatusCode.Created)
            {
                // Delete book if test has failed 
                var bookId = addBadBookResponse.Data.Output.Id;
                _bookHelper.DeleteBook(bookId);
                throw new HttpRequestException("incorrect HTTP response, test fails");
            }
            else
            {
                // Correct status response
                addBadBookResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }






        }

        [Fact]
        public void Scenario_7_As_a_user_I_cannot_add_a_book_with_an_incorrect_body_request()
        {

            // Set up book
            var addBadBook = SetupWithoutSave<AddBookRequest>();

            // Provide body of POST request
            addBadBook.Title = "";
            // addBadBook.Description = "description";
            addBadBook.Author = "Author Name";
            addBadBook.PublishedYear = 2000;
            addBadBook.AvailableFrom = DateTimeOffset.Parse("2022 - 09 - 23T13: 17:10.728Z");
            addBadBook.HasEBook = true;
            addBadBook.BookCategoryId = 1;

            // Call POST API to add book
            var addBadBookResponse = Post<AddBookRequest>(addBadBook, Resources.AddBook, null);

            //Check status response
            addBadBookResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);


        }

    }
}