using System;
using APITestingTemplate.Models.Dtos;
using Audacia.Random.Extensions;
using AutoFixture;
using AutoFixture.Dsl;

namespace APITestingTemplate.DataSetup.Customizations
{
    public class AddBookTitleCustomization : BaseCustomizations
    {
        private Random Random { get; } = new();

        protected override IPostprocessComposer<AddBookRequest> AddBooks(Fixture fixture)
        {
            return base.AddBooks(fixture)
                .With(b => b.Title, () => "Customized Title");
        }
    }
}