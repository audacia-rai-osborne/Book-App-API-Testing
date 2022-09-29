using System;
using System.Net;
using APITestingTemplate.Fixtures;
using APITestingTemplate.Helpers;
using APITestingTemplate.Models.Dtos;
using Audacia.Testing.Api;
using FluentAssertions;
using Xunit;

namespace APITestingTemplate.Tests.BookCategory
{
    public class EditACategoryTest : ApiTestsBase, IClassFixture<AddCategoryFixture>
    {
        private readonly AddCategoryFixture _addCategoryFixture;



        public EditACategoryTest(AddCategoryFixture addCategoryFixture)
        {
            _addCategoryFixture = addCategoryFixture;

        }


        [Fact]
        public void Scenario_16_As_a_user_I_can_edit_a_category()
        {
            // set up edit category request
            var editCategoryRequest = SetupWithoutSave<UpdateBookCategoryRequest>();

            // Get category details
            var categoryDetails = _addCategoryFixture.BookCategoryData;

            //Set category you want to edit
            editCategoryRequest.Id = categoryDetails.Id;
            editCategoryRequest.Name = "New Name";

            // Call the 
            var editCategoryResponse = Put<GetBookCategoryDtoCommandResult>(editCategoryRequest, Resources.EditABookCategory, null);

            // Check the status code is ok
            editCategoryResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // Check that the book category details are correct
            editCategoryResponse.Data.Output.Name.Should().Be("New Name");

        }

        [Fact]
        public void Scenario_17_As_a_user_I_cannot_incorrectly_edit_a_category()
        {
            // set up edit category request
            var editCategoryBadRequest = SetupWithoutSave<UpdateBookCategoryRequest>();
            editCategoryBadRequest.Id = 10;
            editCategoryBadRequest.Name = "";

            // Call the 
            var editCategoryBadResponse = Put<UpdateBookCategoryRequest>(editCategoryBadRequest, Resources.EditABookCategory, null);

            // Check the status code is ok
            editCategoryBadResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            // Check that the book details are correct
            //editBookResponse.Data.Should().Contain(b => b.Title == "ApiTestTitle");

        }

    }
}