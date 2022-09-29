using System;
using System.Collections.Generic;
using System.Net;
using APITestingTemplate.Fixtures;
using APITestingTemplate.Helpers;
using APITestingTemplate.Models.Dtos;
using Audacia.Random.Extensions;
using Audacia.Testing.Api;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace APITestingTemplate.Tests.BookCategory
{
    public class AddABookCategoryTest : ApiTestsBase
    {
        private Random Random { get; } = new();

        private readonly BookCategoryHelper _categoryHelper = new BookCategoryHelper();

        [Fact]
        public void Scenario_14_As_a_user_I_can_add_a_book_category()
        {


            // Set up book category
            var addCategory = SetupWithoutSave<AddBookCategoryRequest>();

            // Provide body of POST request
            addCategory.Name = Random.Words(2);

            // Call the add a book category API
            var addABookCategoryResponse = Post<GetBookCategoryDtoCommandResult>(addCategory, Resources.AddABookCategory, null);

            // Check the status code is ok
            addABookCategoryResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            //Check response by getting category name
            // var addedData = JsonConvert.DeserializeObject<GetBookCategoryDtoCommandResult>(addABookCategoryResponse.Content);
            // addedData.Output.Name.Should().Be("New Category");
            addABookCategoryResponse.Data.Output.Name.Should().Be(addCategory.Name);

            // Delete category
            var categoryId = addABookCategoryResponse.Data.Output.Id;
            _categoryHelper.DeleteBookCategory(categoryId);


        }

        [Fact]
        public void Scenario_15_As_a_user_I_cannot_add_a_book_category_with_incorrect_information()
        {

            // Set up book category
            var addIncorrectCategory = SetupWithoutSave<AddBookCategoryRequest>();

            // Provide body of POST request
            addIncorrectCategory.Name = " ";

            // Call the add a book category API
            var addABookCategoryResponse = Post<AddBookCategoryRequest>(addIncorrectCategory, Resources.AddABookCategory, null);

            // Check the status code is ok
            addABookCategoryResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }
    }
}