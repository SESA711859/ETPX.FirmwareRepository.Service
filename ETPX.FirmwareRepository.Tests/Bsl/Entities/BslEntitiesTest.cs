// Copyright Schneider-Electric 2024

﻿using AutoFixture;
using ETPX.FirmwareRepository.BSL.Entities;

namespace ETPX.FirmwareRepository.Tests.Bsl.Entities
{
    public class BslEntitiesTest
    {
        /// <summary>
        /// Gets or Sets _fixture
        /// </summary>
        private readonly Fixture _fixture;

        public BslEntitiesTest()
        {
            _fixture = new Fixture();
        }

        /// <summary>
        /// Test BslTokenResponse
        /// </summary>
        [Fact]
        public void Test_BslTokenResponse()
        {
            //Arrange
            var obj = _fixture.Build<BslTokenResponse>().Create();

            //Assert
            Assert.NotNull(obj);
            Assert.IsType<BslTokenResponse>(obj);
        }

        [Fact]
        public void Test_BslDocPagesSearchRequest()
        {
            //Arrange
            var obj = _fixture.Build<DocPagesSearchRequest>().Create();

            //Assert
            Assert.NotNull(obj);
            Assert.IsType<DocPagesSearchRequest>(obj);
        }
    }
}
