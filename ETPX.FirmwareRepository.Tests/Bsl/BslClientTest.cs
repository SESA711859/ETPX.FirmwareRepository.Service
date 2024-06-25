// Copyright Schneider-Electric 2024

﻿using AutoFixture;
using ETPX.FirmwareRepository.BSL;
using ETPX.FirmwareRepository.Domain.Entities;
using ETPX.FirmwareRepository.Settings;
using Moq;
using static System.Net.WebRequestMethods;
using File = ETPX.FirmwareRepository.Domain.Entities.File;

namespace ETPX.FirmwareRepository.Tests.Bsl
{
    public class BslClientTest
    {
        private readonly IBslSettings _bslSettings;
        private readonly BslClient _bslClient;
        private readonly Mock<BslClient> _bslClientMock;

        public BslClientTest()
        {
            _bslClientMock = new Mock<BslClient>();
            _bslSettings = BuildBslSettings();
            _bslClient = new BslClient(_bslSettings);
        }

        private IBslSettings BuildBslSettings()
        {
            return new BslSettings
            {
                BaseUrl = "http://abc.com",
                TokenUrl = "http://abc.com",
                GrantType = "Grant Type",
                ClientId = "test",
                ClientSecret = "test",
                FileDownloadBaseUrl ="http://abc.com/files"
            };
        }

        [Fact]
        public void GetDocsByCR_Valid()
        {
            //Arrange
            string commercialReference = "PAS600";

            var files = new Files
            {
                File = new List<File>
                {
                    new File
                    {
                        Extension = ".sedp",
                        Filename = "Test.sedp",
                        Id = "12345",
                        Size = 10000,
                        DownloadLink = "http://abc.com",
                        Sha256 = string.Empty
                    }
                }
            };

            

            _bslClientMock.Setup(s => s.GetDocsByCR(commercialReference))
                .ReturnsAsync(new List<Document> { new Document
                    {
                         DocOid = new object(),
                         Reference ="PAS600",
                         Version = "1.0.0",
                         Files = files
                    }
                });

            //Act
            var result = _bslClient.GetDocsByCR(commercialReference);

            //Assert
            Assert.NotNull(result);
        }
    }
}
