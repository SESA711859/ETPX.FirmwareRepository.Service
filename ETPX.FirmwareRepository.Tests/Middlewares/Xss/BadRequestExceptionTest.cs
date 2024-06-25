// Copyright Schneider-Electric 2024

﻿using ETPX.FirmwareRepository.Middlewares.Xss;

namespace ETPX.FirmwareRepository.Tests.Middlewares.Xss
{
    public class BadRequestExceptionTest
    {
        [Fact]
        public void Test_BadRequestException()
        {
            //Act
            var obj = new BadRequestException("Bad Request", new Exception("Bad Request Exception"));

            //Assert
            Assert.NotNull(obj);
            Assert.IsType<BadRequestException>(obj);
        }
    }
}
