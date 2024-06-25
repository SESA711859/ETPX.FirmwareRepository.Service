// Copyright Schneider-Electric 2024

﻿using AutoFixture;
using ETPX.FirmwareRepository.Domain.Exceptions;
using ETPX.FirmwareRepository.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETPX.FirmwareRepository.Tests.Exceptions
{
    public class NoRecordFoundExceptionTest
    {
        private readonly IFixture _fixture;
        public NoRecordFoundExceptionTest()
        {
            _fixture = new Fixture();

        }
        [Fact]
        public void Test_NoRecordFoundException()
        {
            var obj = _fixture.Build<NoRecordFoundException>().Create();
            //Assert
            Assert.NotNull(obj);
            Assert.IsType<NoRecordFoundException>(obj);
        }
    }
}
