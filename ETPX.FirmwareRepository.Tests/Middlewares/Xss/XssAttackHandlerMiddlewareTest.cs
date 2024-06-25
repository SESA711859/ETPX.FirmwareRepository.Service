// Copyright Schneider-Electric 2024

﻿using AutoFixture;
using ETPX.FirmwareRepository.Middlewares.Xss;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;

namespace ETPX.FirmwareRepository.Tests.Middlewares.Xss
{
    public class XssAttackHandlerMiddlewareTest
    {
        [Fact]
        public async Task Invoke_NoException_CallsNext()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var next = new Mock<RequestDelegate>().Object;
            var logger = new Mock<ILogger<XssAttackHandlerMiddleware>>().Object;
            var middleware = new XssAttackHandlerMiddleware(next, logger);

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
            Dictionary<string, StringValues> queryDictionary = new Dictionary<string, StringValues>
            {
                {"Key1", new StringValues("Value1") },
                {"Key2", new StringValues("Value2") }
            };
            var queryCollection = new QueryCollection(queryDictionary);
            context.Request.Query = queryCollection;
            var exception = new BadRequestException("XSS injection detected from middleware.");
            // Create a list to capture log messages
            var logMessages = new List<string>();

            var loggerMock = new Mock<ILogger<XssAttackHandlerMiddleware>>();
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

            var middleware = new XssAttackHandlerMiddleware(next.Object, logger);

            // Act
            await Assert.ThrowsAsync<BadRequestException>(() => middleware.Invoke(context));
        }

        [Fact]
        public void Test_BadHttpRequestException()
        {
            IFixture _fixture = new Fixture();
            var obj = _fixture.Build<BadHttpRequestException>().Create();

            //Assert
            Assert.NotNull(obj);
            Assert.IsType<BadHttpRequestException>(obj);
        }
    }
}
