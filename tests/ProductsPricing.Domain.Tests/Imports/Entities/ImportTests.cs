using Bogus;
using FluentAssertions;
using ProductsPricing.Core.Exceptions;
using ProductsPricing.Core.ValueObjects;
using ProductsPricing.Domain.Imports.Entities;
using ProductsPricing.UnitTests.Core.Fakers;
using System;
using System.Linq;
using Xunit;

namespace ProductsPricing.Domain.Tests.Imports.Entities
{
    public class ImportTests
    {
        private readonly Faker _faker;
        private readonly ImportFaker _importFaker;
        private readonly EvaluatedImportItemFaker _evaluatedImportItemFaker;
        private readonly PendingImportItemFaker _pendingImportItemFaker;
        private readonly UserFaker _userFaker;
        private readonly Import _item;

        public ImportTests()
        {
            _faker = new Faker();
            _importFaker = new ImportFaker();
            _userFaker = new UserFaker();
            _evaluatedImportItemFaker = new EvaluatedImportItemFaker();
            _pendingImportItemFaker = new PendingImportItemFaker();
            _item = _importFaker.Generate();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_throw_when_invalid_fileName(string fileName)
        {
            // Given / When
            var action = () => new Import(fileName, _userFaker.Generate());

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_when_invalid_user()
        {
            // Given / When
            var action = () => new Import(_item.FileName, null);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_when_file_name_exceeds_max_characters()
        {
            // Given / When
            var action = () => new Import(_faker.Lorem.Letter(Import.FILE_NAME_MAX_LENGTH + 1), _item.User);

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_add_invalid_import_item()
        {
            // Given / When
            var action = () => _item.AddItem(null);

            // Then
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Should_throw_when_mark_as_failure_and_status_is_already_success()
        {
            // Given / When
            _item.AddItem(_evaluatedImportItemFaker.Generate());
            _item.Finish();

            var action = () => _item.MarkAsFailure("Error");

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_mark_as_cancelled_and_status_is_already_success()
        {
            // Given / When
            _item.AddItem(_evaluatedImportItemFaker.Generate());
            _item.Finish();

            var action = () => _item.MarkAsCancelled();

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_throw_when_finish_and_has_pending_items()
        {
            // Given / When
            _item.AddItem(_pendingImportItemFaker.Generate());
            var action = () => _item.Finish();

            // Then
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void Should_add_item()
        {
            // Given
            var importItem = _evaluatedImportItemFaker.Generate();

            // When
            _item.AddItem(importItem);

            // Then
            _item.Items.Should().ContainSingle();
            _item.Items.First().Should().Be(importItem);
        }

        [Fact]
        public void Should_mark_as_failure()
        {
            // Given
            var importItem = _evaluatedImportItemFaker.Generate();
            var errorMessage = "error";
            _item.AddItem(importItem);

            // When
            _item.MarkAsFailure(errorMessage);

            // Then
            _item.Status.IsFailure().Should().BeTrue();
            _item.Logs.Should().Contain(x => x.Message == errorMessage);
            _item.FinishedAt.Should().NotBe(default);
        }

        [Fact]
        public void Should_mark_as_cancelled()
        {
            // Given / When
            _item.MarkAsCancelled();

            // Then
            _item.Status.IsFailure().Should().BeTrue();
            _item.FinishedAt.Should().NotBe(default);
            _item.Logs.Should().Contain(x => x.LogLevel.IsError());
        }

        [Fact]
        public void Should_finish()
        {
            // Given
            var importItem = _evaluatedImportItemFaker.Generate();
            _item.AddItem(importItem);

            // When
            _item.Finish();

            // Then
            _item.Status.IsSuccess().Should().BeTrue();
            _item.FinishedAt.Should().NotBe(default);
        }


        [Fact]
        public void Should_create()
        {
            // Given // When
            var result = new Import(_item.FileName, _item.User);

            // Then
            result.UserId.Should().Be(_item.UserId);
            result.User.Should().Be(_item.User);
            result.FileName.Should().Be(_item.FileName);
            result.StartedAt.Should().NotBe(default);
            result.FinishedAt.Should().Be(default);
            result.Status.IsRunning().Should().BeTrue();
            result.Logs.Should().ContainSingle();
            result.ImpactedProducts.Should().BeEmpty();
        }

        [Fact]
        public void Should_add_information_log()
        {
            // Given // When
            string message = "message";
            _item.AddInformationLog(message);

            // Then
            _item.Logs.Should().HaveCount(2);
            _item.Logs.Last().LogLevel.Should().Be(LogLevel.Information());
            _item.Logs.Last().Message.Should().Be(message);
        }

        [Fact]
        public void Should_add_warning_log()
        {
            // Given // When
            string message = "message";
            _item.AddWarningLog(message);

            // Then
            _item.Logs.Should().HaveCount(2);
            _item.Logs.Last().LogLevel.Should().Be(LogLevel.Warning());
            _item.Logs.Last().Message.Should().Be(message);
        }

        [Fact]
        public void Should_add_error_log()
        {
            // Given // When
            string message = "message";
            _item.AddErrorLog(message);

            // Then
            _item.Logs.Should().HaveCount(2);
            _item.Logs.Last().LogLevel.Should().Be(LogLevel.Error());
            _item.Logs.Last().Message.Should().Be(message);
        }
    }
}