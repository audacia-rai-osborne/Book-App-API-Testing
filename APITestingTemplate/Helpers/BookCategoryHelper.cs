using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using APITestingTemplate.Models.CombinedDtos;
using APITestingTemplate.Models.Dtos;
using Audacia.Random.Extensions;
using Audacia.Testing.Api;
using FluentAssertions;

namespace APITestingTemplate.Helpers
{
    public class BookCategoryHelper : ApiTestsBase, IDisposable
    {
        private Random Random { get; } = new();

        public GetBookCategoryDto AddBookCategory()
        {
            // Set up request to add the category 
            var addBookCategoryRequest = SetupWithoutSave<AddBookCategoryRequest>();
            addBookCategoryRequest.Name = Random.Words(2);

            // Send request to add category
            var addBookCategoryResponse =
                Post<GetBookCategoryDtoCommandResult>(addBookCategoryRequest, Resources.AddABookCategory, null);

            // Check response
            addBookCategoryResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            // Return book values
            return new GetBookCategoryDto()
            {
                Name = addBookCategoryResponse.Data.Output.Name,
                Id = addBookCategoryResponse.Data.Output.Id
            };
        }

        public AddCategoryData AddFiveBookCategories()
        {
            // Set up request to add the category 
            var addBookCategoryRequestOne = SetupWithoutSave<AddBookCategoryRequest>();
            addBookCategoryRequestOne.Name = Random.Words(2);
            var addBookCategoryRequestTwo = SetupWithoutSave<AddBookCategoryRequest>();
            addBookCategoryRequestTwo.Name = Random.Words(2);
            var addBookCategoryRequestThree = SetupWithoutSave<AddBookCategoryRequest>();
            addBookCategoryRequestThree.Name = Random.Words(2);
            var addBookCategoryRequestFour = SetupWithoutSave<AddBookCategoryRequest>();
            addBookCategoryRequestFour.Name = Random.Words(2);
            var addBookCategoryRequestFive = SetupWithoutSave<AddBookCategoryRequest>();
            addBookCategoryRequestFive.Name = Random.Words(2);

            // Send request to add category
            var addBookCategoryResponseOne =
                Post<GetBookCategoryDtoCommandResult>(addBookCategoryRequestOne, Resources.AddABookCategory, null);
            var addBookCategoryResponseTwo =
                Post<GetBookCategoryDtoCommandResult>(addBookCategoryRequestTwo, Resources.AddABookCategory, null);
            var addBookCategoryResponseThree =
                Post<GetBookCategoryDtoCommandResult>(addBookCategoryRequestThree, Resources.AddABookCategory, null);
            var addBookCategoryResponseFour =
                Post<GetBookCategoryDtoCommandResult>(addBookCategoryRequestFour, Resources.AddABookCategory, null);
            var addBookCategoryResponseFive =
                Post<GetBookCategoryDtoCommandResult>(addBookCategoryRequestFive, Resources.AddABookCategory, null);

            // Check response
            addBookCategoryResponseOne.StatusCode.Should().Be(HttpStatusCode.Created);
            addBookCategoryResponseTwo.StatusCode.Should().Be(HttpStatusCode.Created);
            addBookCategoryResponseThree.StatusCode.Should().Be(HttpStatusCode.Created);
            addBookCategoryResponseFour.StatusCode.Should().Be(HttpStatusCode.Created);
            addBookCategoryResponseFive.StatusCode.Should().Be(HttpStatusCode.Created);

            // Return book values
            return new AddCategoryData()
            {
                BookCategoryData = new List<GetBookCategoryDto>()
                {
                    new GetBookCategoryDto()
                    {
                        Name = addBookCategoryResponseOne.Data.Output.Name,
                        Id = addBookCategoryResponseOne.Data.Output.Id
                    },
                    new GetBookCategoryDto()
                    {
                        Name = addBookCategoryResponseTwo.Data.Output.Name,
                        Id = addBookCategoryResponseTwo.Data.Output.Id
                    },
                    new GetBookCategoryDto()
                    {
                        Name = addBookCategoryResponseThree.Data.Output.Name,
                        Id = addBookCategoryResponseThree.Data.Output.Id
                    },
                    new GetBookCategoryDto()
                    {
                        Name = addBookCategoryResponseFour.Data.Output.Name,
                        Id = addBookCategoryResponseFour.Data.Output.Id
                    },
                    new GetBookCategoryDto()
                    {
                        Name = addBookCategoryResponseFive.Data.Output.Name,
                        Id = addBookCategoryResponseFive.Data.Output.Id
                    }
                }
            };

        }
        public void DeleteBookCategory(int bookCategoryId)
        {
            //Send request to delete the book category
            var deleteBookCategoryResponse =
                Delete<GetBookCategoryDtoCommandResult>(bookCategoryId, Resources.DeleteCategory, null);

            // Check the correct response is returned
            deleteBookCategoryResponse.StatusCode.Should().Be(HttpStatusCode.OK);


        }
    }

   
}