// Copyright Schneider-Electric 2024

﻿using AutoFixture;
using Castle.Core.Internal;
using ETPX.FirmwareRepository.Domain.Query;
using ETPX.FirmwareRepository.Entities;
using ETPX.FirmwareRepository.ServiceProvider;
using Moq;
using static ETPX.FirmwareRepository.Domain.Query.GetSpecificFirmwareVersionQuery;

namespace ETPX.FirmwareRepository.Tests.Domain.Query
{
    public class GetSpecificFirmwareVersionQueryTest
    {
        /// <summary>
        /// Gets or Sets _firmwareVersionProvider
        /// </summary>
        private readonly Mock<IFirmwareVersionProvider> _firmwareVersionProvider;

        /// <summary>
        /// Get or Sets _handler
        /// </summary>
        private readonly GetSpecificFirmwareVersionQueryHandler _handler;

        /// <summary>
        /// Get or Sets _cancellationToken
        /// </summary>
        private readonly CancellationToken _cancellationToken;

        /// <summary>
        /// Gets or Sets _fixture
        /// </summary>
        private readonly Fixture _fixture;

        /// <summary>
        /// Get or Sets _query
        /// </summary>
        private GetSpecificFirmwareVersionQuery _query;

        /// <summary>
        /// Gets or Sets _commercialReference
        /// </summary>
        private readonly string _commercialReference;

        /// <summary>
        /// Gets or Sets _version
        /// </summary>
        private readonly Version _version;

        public GetSpecificFirmwareVersionQueryTest()
        {
            _fixture = new Fixture();
            _firmwareVersionProvider = new Mock<IFirmwareVersionProvider>();
            _handler = new GetSpecificFirmwareVersionQueryHandler(_firmwareVersionProvider.Object);
            _cancellationToken = new CancellationToken();
            _commercialReference = string.Empty;
            _version = new Version("1.0.0");
            _query = new GetSpecificFirmwareVersionQuery(_commercialReference, _version);
        }

        /// <summary>
        /// GetSpecificFirmwareVersionQuery - - Returns data
        /// </summary>
        [Fact]
        public async void GetSpecificFirmwareVersionQuery_ReturnsValidData()
        {
            // Arrange
            var data = _fixture
               .Build<FirmwareDetail>()
               .With(p => p.Version, "1.0.0" )
               .CreateMany(2).ToList();

            _firmwareVersionProvider
                .Setup(m => m.GetSpecificFwAsync(_commercialReference, _version))
                .ReturnsAsync(data)
                .Verifiable();

            // Act
            var result = await _handler.Handle(_query, _cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.True(!result.FirmwareList.FirstOrDefault()?.Version.IsNullOrEmpty());
            Assert.Equal("1.0.0", result.FirmwareList.FirstOrDefault()?.Version.ToString());
        }
    }
}
