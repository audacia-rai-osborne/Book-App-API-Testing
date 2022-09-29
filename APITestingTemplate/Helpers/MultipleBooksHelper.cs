using System;
using System.Collections.Generic;
using System.Net;
using APITestingTemplate.Models.CombinedDtos;
using APITestingTemplate.Models.Dtos;
using Audacia.Random.Extensions;
using Audacia.Testing.Api;
using FluentAssertions;

namespace APITestingTemplate.Helpers
{
    public class MultipleBooksHelper : ApiTestsBase, IDisposable
    {
        private Random Random { get; } = new();

        private readonly BookCategoryHelper _bookCategoryHelper;
        public MultipleBooksHelper()
        {
            _bookCategoryHelper = new BookCategoryHelper();
        }

        private AddBookandCategoryData CreateBook(int bookCategoryId, string bookCategoryName)
        {
            // Set up the request to add the book
            var addBookRequest = SetupWithoutSave<AddBookRequest>();

            // Set category to be what has been created
            addBookRequest.BookCategoryId = bookCategoryId;
            addBookRequest.Title = Random.Words(2);
            addBookRequest.Description = Random.Sentence();
            addBookRequest.Author = Random.Forename() + ' ' + Random.Surname();
            addBookRequest.PublishedYear = 2005;
            addBookRequest.HasEBook = true;
            addBookRequest.AvailableFrom = DateTime.Parse("2022-09-16T12:55:22.1172");

            // Send the request to add the book
            var addBookResponse =
                Post<GetBookDtoCommandResult>(addBookRequest, Resources.AddBook, null);

            // Check the correct response is returned
            addBookResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            // Return the book values as the GetBookDto
            return new AddBookandCategoryData()
            {
                BookData = new List<GetBookDto>()
               {
                   addBookResponse.Data.Output
               },

                BookCategoryData = new List<GetBookCategoryDto>(){
                   new GetBookCategoryDto()
                   {
                       Name = bookCategoryName,
                       Id = bookCategoryId

                   }
            }
            };
        }

        public AddBookandCategoryData AddBookWithBookCategory()
        {
            var bookCategory = _bookCategoryHelper.AddBookCategory();
            return CreateBook(bookCategory.Id, bookCategory.Name);
        }

        public void DeleteBook(int bookId)
        {
            // Call the API to delete a book
            var deleteBookResponse = Delete(bookId, Resources.DeleteBook, null);

            // Check the correct response is returned
            deleteBookResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        public void DeleteBookandCategory(int bookCategoryId, int bookId)
        {
            DeleteBook(bookId);

            _bookCategoryHelper.DeleteBookCategory(bookCategoryId);
        }
    }
}
