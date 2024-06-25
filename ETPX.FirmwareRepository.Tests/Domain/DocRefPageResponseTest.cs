// Copyright Schneider-Electric 2024

﻿using AutoFixture;
using ETPX.FirmwareRepository.Domain.Entities;

namespace ETPX.FirmwareRepository.Tests.Domain
{
    public class DocRefPageResponseTest
    {
        private readonly IFixture _fixture;

        public DocRefPageResponseTest() 
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void DocRefPageResponse_Test()
        {
            //Arrange
            var obj = _fixture.Build<DocRefPageResponse>().Create();

            //Assert
            Assert.NotNull(obj);
            Assert.IsType<DocRefPageResponse>(obj);
        }

    }
}
