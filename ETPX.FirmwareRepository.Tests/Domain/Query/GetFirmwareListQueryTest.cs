// Copyright Schneider-Electric 2024

﻿using AutoFixture;
using ETPX.FirmwareRepository.Domain.Query;
using ETPX.FirmwareRepository.Entities;
using ETPX.FirmwareRepository.ServiceProvider;
using Moq;
using static ETPX.FirmwareRepository.Domain.Query.GetFirmwareListQuery;

namespace ETPX.FirmwareRepository.Tests.Domain.Query
{
    public class GetFirmwareListQueryTest
    {
        /// <summary>
        /// Gets or Sets _firmwareVersionProvider
        /// </summary>
        private readonly Mock<IFirmwareVersionProvider> _firmwareVersionProvider;

        /// <summary>
        /// Get or Sets _handler
        /// </summary>
        private readonly GetFirmwareListQueryHandler _handler;

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
        private GetFirmwareListQuery _query;

        /// <summary>
        /// Gets or Sets _commercialReference
        /// </summary>
        private readonly string _commercialReference;

        public GetFirmwareListQueryTest()
        {
            _fixture = new Fixture();
            _firmwareVersionProvider = new Mock<IFirmwareVersionProvider>();
            _handler = new GetFirmwareListQueryHandler(_firmwareVersionProvider.Object);
            _cancellationToken = new CancellationToken();
            _commercialReference = string.Empty;
            _query = new GetFirmwareListQuery(_commercialReference);
        }

        /// <summary>
        /// GetFirmwareListQuery - - Returns data
        /// </summary>
        [Fact]
        public async void GetFirmwareListQuery_ReturnsList()
        {
            // Arrange
            var data = _fixture
                .Build<FirmwareDetail>()
                .With(p => p.Version, string.Empty)
                .CreateMany(10).ToList();

            _firmwareVersionProvider
                .Setup(m => m.GetFirmwareListAsync(_commercialReference))
                .ReturnsAsync(data)
                .Verifiable();

            // Act
            var result = await _handler.Handle(_query, _cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.FirmwareList.Count == 10);
        }
    }
}
