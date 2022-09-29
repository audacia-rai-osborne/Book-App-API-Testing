using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using APITestingTemplate.Fixtures;
using APITestingTemplate.Helpers;
using APITestingTemplate.Models.Dtos;
using Audacia.Extensions;
using Audacia.Testing.Api;
using FluentAssertions;
using Xunit;

namespace APITestingTemplate.Tests.BookCategory
{
    public class GetAllBookCategoriesTest : ApiTestsBase, IClassFixture<AddMultipleCategoriesFixture>
    {


        private readonly AddMultipleCategoriesFixture _addMultipleCategoriesFixture;


        public GetAllBookCategoriesTest(AddMultipleCategoriesFixture addMultipleCategoriesFixture)
        {

            _addMultipleCategoriesFixture = addMultipleCategoriesFixture;

        }

        [Fact]
        public void Scenario_12_As_a_user_I_can_get_all_book_categories()
        {

            //Get category details
            var getCategoryIdOne = _addMultipleCategoriesFixture.BookCategoryData.BookCategoryData.First().Id;
            var getCategoryIdTwo = _addMultipleCategoriesFixture.BookCategoryData.BookCategoryData.ElementAt(1).Id;
            var getCategoryIdThree = _addMultipleCategoriesFixture.BookCategoryData.BookCategoryData.ElementAt(2).Id;
            var getCategoryIdFour = _addMultipleCategoriesFixture.BookCategoryData.BookCategoryData.ElementAt(3).Id;
            var getCategoryIdFive = _addMultipleCategoriesFixture.BookCategoryData.BookCategoryData.ElementAt(4).Id;


            // Call the get all book categories
            var getAllBookCategoriesResponse = GetAll<List<GetBookCategoryDto>>(Resources.GetAllBookCategories, null);

            // Check the status code is ok
            getAllBookCategoriesResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // Check that the book category details are correct
            getAllBookCategoriesResponse.Data.Should().Contain(a => a.Id == getCategoryIdOne || a.Id == getCategoryIdTwo || a.Id == getCategoryIdThree || a.Id == getCategoryIdFour || a.Id == getCategoryIdFive);
        }
    }
}