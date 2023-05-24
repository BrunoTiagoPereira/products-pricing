using System;
using System.Threading.Tasks;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Http;

using ProductsPricing.Api.Middlewares;
using ProductsPricing.Core.Exceptions;
using Xunit;

namespace ProductsPricing.Api.Tests.Middlewares
{
    public class ExceptionHandlingMiddlewareTests
    {
        [Fact]
        public async Task Should_handle_DomainException()
        {
            // given
            var middleware = new ExceptionHandlingMiddleware();
            var context = new DefaultHttpContext();

            // when
            var action = () => middleware.InvokeAsync(context, (_) => throw new DomainException("Error"));

            // then
            await action.Should().NotThrowAsync();
            context.Response.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Should_handle_EntityNotFoundException()
        {
            // given
            var middleware = new ExceptionHandlingMiddleware();
            var context = new DefaultHttpContext();

            // when
            var action = () => middleware.InvokeAsync(context, (_) => throw new NotFoundException("Error"));

            // then
            await action.Should().NotThrowAsync();
            context.Response.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Should_handle_Exception()
        {
            // given
            var middleware = new ExceptionHandlingMiddleware();
            var context = new DefaultHttpContext();

            // when
            var action = () => middleware.InvokeAsync(context, (_) => throw new Exception());

            // then
            await action.Should().NotThrowAsync();
            context.Response.StatusCode.Should().Be(500);
        }
    }
}