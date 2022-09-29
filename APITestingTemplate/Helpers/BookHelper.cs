using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Transactions;
using APITestingTemplate.Models.CombinedDtos;
using APITestingTemplate.Models.Dtos;
using Audacia.Random.Extensions;
using Audacia.Testing.Api;
using FluentAssertions;

namespace APITestingTemplate.Helpers
{
    public class BookHelper : ApiTestsBase, IDisposable
    {
        private Random Random {get;} = new();

        private readonly BookCategoryHelper _bookCategoryHelper;
        public BookHelper()
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

        public AddBookandCategoryData AddFiveBooks()
        {
            var bookCategory = _bookCategoryHelper.AddBookCategory();

            AddBookandCategoryData bookOne = CreateBook(bookCategory.Id, bookCategory.Name);
            AddBookandCategoryData bookTwo = CreateBook(bookCategory.Id, bookCategory.Name);
            AddBookandCategoryData bookThree = CreateBook(bookCategory.Id, bookCategory.Name);
            AddBookandCategoryData bookFour = CreateBook(bookCategory.Id, bookCategory.Name);
            AddBookandCategoryData bookFive = CreateBook(bookCategory.Id, bookCategory.Name);
            return new AddBookandCategoryData()
            {
                BookData = new List<GetBookDto>()
                {
                    bookOne.BookData.First(),
                    bookTwo.BookData.First(),
                    bookThree.BookData.First(),
                    bookFour.BookData.First(),
                    bookFive.BookData.First()
                },
                BookCategoryData = new List<GetBookCategoryDto>()
                {
                    new GetBookCategoryDto()
                    {
                        Name = bookOne.BookCategoryData.First().Name,
                        Id = bookOne.BookCategoryData.First().Id
                    },

                }
            }; }

        public AddBookandCategoryData AddFiveBooksandCategories()
        {
            var bookCategoryOne = _bookCategoryHelper.AddBookCategory();
            var bookCategoryTwo = _bookCategoryHelper.AddBookCategory();
            var bookCategoryThree = _bookCategoryHelper.AddBookCategory();
            var bookCategoryFour = _bookCategoryHelper.AddBookCategory();
            var bookCategoryFive = _bookCategoryHelper.AddBookCategory();

            AddBookandCategoryData bookOne = CreateBook(bookCategoryOne.Id, bookCategoryOne.Name);
            AddBookandCategoryData bookTwo = CreateBook(bookCategoryTwo.Id, bookCategoryTwo.Name);
            AddBookandCategoryData bookThree = CreateBook(bookCategoryThree.Id, bookCategoryThree.Name);
            AddBookandCategoryData bookFour = CreateBook(bookCategoryFour.Id, bookCategoryFour.Name);
            AddBookandCategoryData bookFive = CreateBook(bookCategoryFive.Id, bookCategoryFive.Name);
            return new AddBookandCategoryData()
            {
                BookData = new List<GetBookDto>()
                {
                    bookOne.BookData.First(),
                    bookTwo.BookData.First(),
                    bookThree.BookData.First(),
                    bookFour.BookData.First(),
                    bookFive.BookData.First()
                },
                BookCategoryData = new List<GetBookCategoryDto>()
                {
                    new GetBookCategoryDto()
                    {
                        Name = bookOne.BookCategoryData.First().Name,
                        Id = bookOne.BookCategoryData.First().Id
                    },
                    new GetBookCategoryDto()
                    {
                        Name = bookTwo.BookCategoryData.First().Name,
                        Id = bookTwo.BookCategoryData.First().Id
                    },
                    new GetBookCategoryDto()
                    {
                        Name = bookThree.BookCategoryData.First().Name,
                        Id = bookThree.BookCategoryData.First().Id
                    },
                    new GetBookCategoryDto()
                    {
                        Name = bookFour.BookCategoryData.First().Name,
                        Id = bookFour.BookCategoryData.First().Id
                    },
                    new GetBookCategoryDto()
                    {
                        Name = bookFive.BookCategoryData.First().Name,
                        Id = bookFive.BookCategoryData.First().Id
                    },

                }
            };
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
