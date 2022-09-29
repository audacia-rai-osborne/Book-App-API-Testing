using System;
using APITestingTemplate.Models.Dtos;
using Audacia.Random.Extensions;
using AutoFixture;
using AutoFixture.Dsl;

namespace APITestingTemplate.DataSetup.Customizations
{
    public class AddBadDateCustomization : BaseCustomizations
    {
        private Random Random { get; } = new();

        protected override IPostprocessComposer<AddBookRequest> AddBooks(IFixture fixture)
        {
            return base.AddBooks(fixture)
                .With(b => b.PublishedYear, () => -2000);
        }
    }

}