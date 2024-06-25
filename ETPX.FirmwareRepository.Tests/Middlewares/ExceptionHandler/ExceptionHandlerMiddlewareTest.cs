// Copyright Schneider-Electric 2024

﻿using AutoFixture;
using ETPX.FirmwareRepository.Middlewares.ExceptionHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace ETPX.FirmwareRepository.Tests.Middlewares.ExceptionHandler
{
    public class ExceptionHandlerMiddlewareTest
    {
        [Fact]
        public async Task Invoke_NoException_CallsNext()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var next = new Mock<RequestDelegate>().Object;
            var logger = new Mock<ILogger<ExceptionHandlerMiddleware>>().Object;
            var middleware = new ExceptionHandlerMiddleware(next, logger);

            // Act
            await middleware.Invoke(context);

            // Assert
            // Verify that the middleware calls the next middleware in the pipeline
            Mock.Get(next).Verify(next => next(context), Times.Once());
        }

        [Fact]
        public async Task Invoke_Exception_LogsAndHandlesException()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var exception = new Exception("Test Exception");
            // Create a list to capture log messages
            var logMessages = new List<string>();

            var loggerMock = new Mock<ILogger<ExceptionHandlerMiddleware>>();
            loggerMock
            .Setup(x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<object>(),
                exception,
                (Func<object, Exception?, string>)It.IsAny<object>()
            ))
            .Callback((LogLevel level, EventId eventId, object state, Exception ex, Func<object, Exception, string> formatter) =>
            {
                // Capture the log message
                logMessages.Add(formatter(state, ex));
            });
         
            var logger = loggerMock.Object;

            var next = new Mock<RequestDelegate>();
            next.Setup(next => next(context)).Throws(exception);

            var middleware = new ExceptionHandlerMiddleware(next.Object, logger);

            // Act
            await Assert.ThrowsAsync<ArgumentException>(() => middleware.Invoke(context));
        }

        [Fact]
        public void Test_Error()
        {
            IFixture _fixture = new Fixture();
            var obj = _fixture.Build<Error>().Create();

            //Assert
            Assert.NotNull(obj);
            Assert.IsType<Error>(obj);
        }
    }
}
