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
    public class DeleteABookTest : ApiTestsBase, IClassFixture<AddBookFixture>
    {
        private readonly AddBookFixture _addBookFixture;

        public DeleteABookTest(AddBookFixture addBookFixture)
        {
            _addBookFixture = addBookFixture;
        }

        [Fact]
        public void Scenario_10_As_A_User_I_Can_Delete_A_Book()
        {
            // Get book details
            var bookDetails = _addBookFixture.BookData.BookData;

            //Set Id you want to get
            var bookId = bookDetails.First().Id;

            //delete book
            var deleteBookResponse = Delete<Dictionary<string, object>>(bookId, Resources.DeleteBook, null);

            //check response
            deleteBookResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public void Scenario_11_As_A_User_I_Cannot_Delete_A_Book_without_An_Id()
        {
            //set bookID
            var bookId = 456;

            //call DELETE API
            var deleteBookResponse = Delete<Dictionary<string, object>>(bookId, Resources.DeleteBook, null);

            //check response
            deleteBookResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }


    }
}