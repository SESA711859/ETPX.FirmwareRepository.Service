// Copyright Schneider-Electric 2024

﻿using AutoFixture;
using ETPX.FirmwareRepository.Settings;

namespace ETPX.FirmwareRepository.Tests.Settings
{
    public class SettingsTest
    {
        /// <summary>
        /// Gets or Sets _fixture
        /// </summary>
        private readonly Fixture _fixture;

        public SettingsTest()
        {
            _fixture = new Fixture();
        }

        /// <summary>
        /// Test BslSettings
        /// </summary>
        [Fact]
        public void Test_BslSettings()
        {
            //Arrange
            var obj = _fixture.Build<BslSettings>().Create();

            //Assert
            Assert.NotNull(obj);
            Assert.IsType<BslSettings>(obj);
        }

        /// <summary>
        /// Test SwaggerSettings
        /// </summary>
        [Fact]
        public void Test_SwaggerSettings()
        {
            //Arrange
            var obj = _fixture.Build<SwaggerSettings>().Create();

            //Assert
            Assert.NotNull(obj);
            Assert.IsType<SwaggerSettings>(obj);
        }
    }
}
