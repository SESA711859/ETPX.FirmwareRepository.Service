// Copyright Schneider-Electric 2024

﻿using AutoFixture;
using ETPX.FirmwareRepository.Domain.Exceptions;

namespace ETPX.FirmwareRepository.Tests.Exceptions
{
    public class TokenExpiredExceptionTest
    {
        private readonly IFixture _fixture;
        public TokenExpiredExceptionTest()
        {
            _fixture = new Fixture();

        }
        [Fact]
        public void Constructor_Call_Throw_NoException()
        {
            var obj = _fixture.Build<TokenExpiredException>().Create();
            //Assert
            Assert.NotNull(obj);
            Assert.IsType<TokenExpiredException>(obj);
        }
    }
}
