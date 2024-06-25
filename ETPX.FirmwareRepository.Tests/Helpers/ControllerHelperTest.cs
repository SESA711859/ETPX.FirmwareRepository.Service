// Copyright Schneider-Electric 2024

﻿using ETPX.FirmwareRepository.Entities;
using ETPX.FirmwareRepository.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETPX.FirmwareRepository.Tests.Helpers
{
    public class ControllerHelperTest
    {
        private readonly Mock<ILogger> _logger;

        public ControllerHelperTest()
        {
            _logger = new Mock<ILogger>();
        }

        [Fact]
        public void LogNotFoundMessage_Test()
        {
            //Act
            var response = ControllerHelper.LogNotFoundMessage(_logger.Object, nameof(LogNotFoundMessage_Test), EntityType.CommercialReferenceNo);

            //Assert
            Assert.NotNull(response);
            Assert.Equal("CommercialReferenceNo not found.", response.ErrorMessages.FirstOrDefault());
        }

        [Fact]
        public void LogBadInputMessage_Test()
        {
            //Act
            var response = ControllerHelper.LogBadInputMessage(_logger.Object, nameof(LogBadInputMessage_Test), EntityType.CommercialReferenceNo);

            //Assert
            Assert.NotNull(response);
            Assert.Equal("CommercialReferenceNo is invalid.", response.ErrorMessages.FirstOrDefault());
        }

        [Fact]
        public void LogInValidInputMessage_Test()
        {
            //Act
            var response = ControllerHelper.LogInValidInputMessage(_logger.Object, nameof(LogInValidInputMessage_Test), EntityType.FirmwareVersion);

            //Assert
            Assert.NotNull(response);
            Assert.Equal("FirmwareVersion is invalid.", response.ErrorMessages.FirstOrDefault());
        }

        [Fact]
        public void LogValidationErrorMessages_Test()
        {
            //Arrange
            var errorMessages = new List<string> { "Invalid data" };

            //Act
            var response = ControllerHelper.LogValidationErrorMessages(_logger.Object, nameof(LogValidationErrorMessages_Test), errorMessages);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(errorMessages, response.Result);
        }

        [Fact]
        public void LogCreateUpdateMessage_Test()
        {

            //Act
            ControllerHelper.LogCreateUpdateMessage(_logger.Object, EntityType.CommercialReferenceNo, "entity value");

            // Assert
            // Verify that LogCreateUpdateMessage method is called
            _logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.IsAny<object>(),
                    It.IsAny<Exception>(),
                    (Func<object, Exception?, string>)It.IsAny<object>()),
                Times.Once());
        }

        [Fact]
        public void LogGetResponseMessage_Test()
        {
            //Act
            ControllerHelper.LogGetResponseMessage(_logger.Object, nameof(LogGetResponseMessage_Test), EntityType.CommercialReferenceNo, "entity value");

            // Assert
            // Verify that LogGetResponseMessage method is called
            _logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.IsAny<object>(),
                    It.IsAny<Exception>(),
                    (Func<object, Exception?, string>)It.IsAny<object>()),
                Times.Once());
        }
    }    
}
