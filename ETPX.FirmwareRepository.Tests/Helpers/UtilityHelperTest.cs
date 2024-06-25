// Copyright Schneider-Electric 2024

﻿using ETPX.FirmwareRepository.Helpers;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace ETPX.FirmwareRepository.Tests.Helpers
{
    public class UtilityHelperTest
    {
        private readonly Mock<UtilityHelper> _utilityHelperMock;
        private readonly UtilityHelper _utilityHelper;

        public UtilityHelperTest()
        {
            _utilityHelperMock = new Mock<UtilityHelper>();
            _utilityHelper = new UtilityHelper();
        }


        [Fact]
        public void ParseVersion_Valid()
        {
            //Arrange
            string version = "V 2";

            _utilityHelperMock.Setup(s => s.ParseVersion(version)).Returns(new Version("1.0.0.0"));

            //Act
            var result = _utilityHelper.ParseVersion(version);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result, new Version("2.0.0"));
        }

        [Fact]
        public void ParseVersion_Major_Minor_Valid()
        {
            //Arrange
            string version = "2.0.0";

            _utilityHelperMock.Setup(s => s.ParseVersion(version)).Returns(new Version("2.0.0"));

            //Act
            var result = _utilityHelper.ParseVersion(version);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result, new Version("2.0.0"));
        }

        [Fact]
        public void ParseVersion_FormatException_ReturnsDefaultVersion()
        {
            //Arrange
            var versionParser = new Mock<UtilityHelper>();
            versionParser.CallBase = true;

            string version = "InvalidVersion";
            //Act
            Version parsedVersion = versionParser.Object.ParseVersion(version);

            //Assert
            Assert.Equal(new Version("1.0.0.0"), parsedVersion);
        }
    }
}
