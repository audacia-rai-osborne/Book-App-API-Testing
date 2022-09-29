﻿using System;
using APITestingTemplate.Fixtures;
using APITestingTemplate.Models.Dtos;
using Audacia.Random.Extensions;
using Audacia.Testing.Api.DataSetup;
using AutoFixture;
using AutoFixture.Dsl;

namespace APITestingTemplate.DataSetup.Customizations
{
    [DefaultCustomization]
    public class BaseCustomizations: ICustomization
    {
        private Random Random { get; } = new();

        public void Customize(IFixture fixture)
        {
            // Books
            fixture.Register(() =>
                        AddBooks(fixture).Create())

            ;
        }

        protected virtual IPostprocessComposer<AddBookRequest>AddBooks(IFixture fixture){
                return fixture.Build<AddBookRequest>()
                    .With(dto => dto.Title, () => Random.Words(2))
                    .With(dto => dto.Description, () => Random.Sentence())
                    .With(dto => dto.Author, () => Random.FemaleForename() + ' ' + Random.Surname())
                    .With(dto => dto.PublishedYear, () => 2015)
                    .With(dto => dto.HasEBook, () => true)
                    .With(dto => dto.AvailableFrom, () => DateTime.Parse("2022-09-16T12:55:22.117Z"))
                    .With(dto => dto.BookCategoryId, () => 1).Create());
        }
    }
}
