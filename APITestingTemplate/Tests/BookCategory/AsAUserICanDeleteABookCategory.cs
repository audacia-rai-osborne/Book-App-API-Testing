using System.Collections.Generic;
using System.Net;
using APITestingTemplate.Fixtures;
using APITestingTemplate.Helpers;
using APITestingTemplate.Models.Dtos;
using Audacia.Testing.Api;
using FluentAssertions;
using Xunit;

namespace APITestingTemplate.Tests.BookCategory
{
    public class DeleteABookCategoryTest : ApiTestsBase, IClassFixture<AddCategoryFixture>
    {
        private readonly AddCategoryFixture _addCategoryFixture;

        public DeleteABookCategoryTest(AddCategoryFixture addCategoryFixture)
        {
            _addCategoryFixture = addCategoryFixture;
        }

        [Fact]
        public void Scenario_18_As_a_user_I_can_delete_a_book_category()
        {

            // Get category details
            var categoryDetails = _addCategoryFixture.BookCategoryData;

            //specify categoryId
            var categoryId = categoryDetails.Id;

            //delete book
            var deleteCategoryResponse = Delete<Dictionary<string, object>>(categoryId, Resources.DeleteCategory, null);

            //check response
            deleteCategoryResponse.StatusCode.Should().Be(HttpStatusCode.OK);


        }

        [Fact]
        public void Scenario_19_As_a_user_I_cannot_delete_a_book_category_that_doesnt_exist()
        {

            //specify categoryId
            var categoryId = 7000;

            //delete book
            var deleteCategoryResponse = Delete<Dictionary<string, object>>(categoryId, Resources.DeleteCategory, null);

            //check response
            deleteCategoryResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);


        }


    }
}