// Copyright Schneider-Electric 2024

using AutoFixture;
using AutoMapper;
using ETPX.FirmwareRepository.BSL;
using ETPX.FirmwareRepository.Domain.Entities;
using ETPX.FirmwareRepository.Entities;
using ETPX.FirmwareRepository.Exceptions;
using ETPX.FirmwareRepository.Helpers;
using ETPX.FirmwareRepository.ServiceProvider;
using ETPX.FirmwareRepository.Settings;
using Moq;
using File = ETPX.FirmwareRepository.Domain.Entities.File;

namespace ETPX.FirmwareRepository.Tests.ServiceProvider
{
    public class FirmwareVersionProviderTest
    {
        private readonly Mock<BslClient> _bslclient;
        private readonly Mock<UtilityHelper> _utilityHelper;
        private readonly FirmwareVersionProvider _firmwareVersionProvider;
        private readonly Fixture _fixture;
        private readonly Mock<IBslSettings> _bslSettings;

        public FirmwareVersionProviderTest()
        {
            IBslSettings bslSettings = new BslSettings
            {
                TokenUrl = "http://abc.com",
                BaseUrl = "http://abc.com",
                GrantType = "grand type"
            };

            _bslclient = new Mock<BslClient>(bslSettings);
            _utilityHelper = new Mock<UtilityHelper>();
            _fixture = new Fixture();
            _bslSettings = new Mock<IBslSettings>();

            _firmwareVersionProvider = new FirmwareVersionProvider(_bslclient.Object, _utilityHelper.Object, _bslSettings.Object);
        }

        [Fact]
        public void GetFirmwareList_Valid()
        {
            // Arrange
            string commercialReference = "PAS600";

            var documents = _fixture
                .Build<Document>()
                .With(d => d.Reference, commercialReference)
                .CreateMany(1).ToList();

            Assert.NotNull(documents);
            Assert.IsType<Document>(documents[0]);

            var firmVersionProviderMock = new Mock<IFirmwareVersionProvider>();
            firmVersionProviderMock.Setup(s => s.GetFirmwareListAsync(commercialReference))
            .ReturnsAsync(new List<FirmwareDetail> { new FirmwareDetail
                    {
                          DownloadUrl ="http://abc.com",
                          Description ="Test",
                          FileExtension =".sedp",
                          LastModifiedDate = DateTime.Now,
                          FileName = "Test.sedp",
                          Version = "1.0.0",
                          CheckSumSHA256 = "343434343434343"
                    }
                }).Verifiable();

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

            _bslclient.Setup(s => s.GetDocsByCR(commercialReference))
                .ReturnsAsync(new List<Document> { new Document
                    {
                         DocOid = new object(),
                         Reference ="PAS600",
                         Version = "1.0.0",
                         Files = files
                    }
                });


            _utilityHelper.Setup(s => s.ParseVersion(It.IsAny<string>())).Returns(new Version("1.0.0"));

            //Act
            var result = _firmwareVersionProvider.GetFirmwareListAsync(commercialReference);

            //Assert
            Assert.NotNull(result.Result);
            Assert.IsType<List<FirmwareDetail>>(result.Result);
            Assert.True(result.Result.Count > 0);
        }

        [Fact]
        public void GetLatestFirmware_Valid()
        {
            // Arrange
            string commercialReference = "PAS600";

            var firmVersionProviderMock = new Mock<IFirmwareVersionProvider>();
            firmVersionProviderMock.Setup(s => s.GetLatestFirmwareAsync(commercialReference, null))
            .ReturnsAsync(new List<FirmwareDetail>() { new FirmwareDetail
            {
                DownloadUrl = "http://abc.com",
                Description = "Test",
                FileExtension = ".sedp",
                LastModifiedDate = DateTime.Now,
                FileName = "Test.sedp",
                Version = "1.0.0",
                CheckSumSHA256 = "343434343434343"
            } })
            .Verifiable();

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

            _bslclient.Setup(s => s.GetDocsByCR(commercialReference))
                .ReturnsAsync(new List<Document> { new Document
                    {
                         DocOid = new object(),
                         Reference ="PAS600",
                         Version = "1.0.0",
                         Files = files
                    }
                });

            _utilityHelper.Setup(s => s.ParseVersion(It.IsAny<string>())).Returns(new Version("1.0.0"));


            //Act
            var result = _firmwareVersionProvider.GetLatestFirmwareAsync(commercialReference, null);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.IsType<List<FirmwareDetail>>(result.Result);
        }

        [Fact]
        public async void GetLatestFirmware_InValid()
        {
            // Arrange
            string commercialReference = "PAS600";
            Version version = new Version("2.0.0");

            var firmVersionProviderMock = new Mock<IFirmwareVersionProvider>();
            firmVersionProviderMock.Setup(s => s.GetLatestFirmwareAsync(commercialReference, version))
            .ReturnsAsync(new List<FirmwareDetail>() { new FirmwareDetail
            {
                DownloadUrl = "http://abc.com",
                Description = "Test",
                FileExtension = ".sedp",
                LastModifiedDate = DateTime.Now,
                FileName = "Test.sedp",
                Version = "1.0.0",
                CheckSumSHA256 = "343434343434343"
            }})
            .Verifiable();

            _bslclient.Setup(s => s.GetDocsByCR(commercialReference))
                .ReturnsAsync(new List<Document> {});

            _utilityHelper.Setup(s => s.ParseVersion(It.IsAny<string>())).Returns(new Version("1.0.0"));


            //Act
            var act =  () => _firmwareVersionProvider.GetLatestFirmwareAsync(commercialReference, version);

            NoRecordFoundException exception = await Assert.ThrowsAsync<NoRecordFoundException>(act);
            //Assert
            Assert.Equal(Constants.Latest_FW_Not_Found_Message, exception.Message);
            
        }

        [Fact]
        public void GetSpecificFirmware_Valid()
        {
            // Arrange
            string commercialReference = "PAS600";
            var version = new Version("1.0.0");

            var firmVersionProviderMock = new Mock<IFirmwareVersionProvider>();
            firmVersionProviderMock.Setup(s => s.GetSpecificFwAsync(commercialReference, version))
            .ReturnsAsync(new List<FirmwareDetail>() {
                 new FirmwareDetail
                {
                    DownloadUrl = "http://abc.com",
                    Description = "Test",
                    FileExtension = ".sedp",
                    LastModifiedDate = DateTime.Now,
                    FileName = "Test.sedp",
                    Version = "1.0.0",
                    CheckSumSHA256 = "343434343434343"
                }
                }).Verifiable();

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

            _bslclient.Setup(s => s.GetDocsByCR(commercialReference))
                .ReturnsAsync(new List<Document> { new Document
                    {
                         DocOid = new object(),
                         Reference ="PAS600",
                         Version = "1.0.0",
                         Files = files
                    }
                });

            _utilityHelper.Setup(s => s.ParseVersion(It.IsAny<string>())).Returns(new Version("1.0.0"));

            //Act
            var result = _firmwareVersionProvider.GetSpecificFwAsync(commercialReference, version);

            //Assert
            Assert.NotNull(result.Result);
            Assert.IsType<List<FirmwareDetail>>(result.Result);
        }

        [Fact]
        public async void GetSpecificFirmware_Invalid()
        {
            // Arrange
            string commercialReference = "Test123";
            var version = new Version("1.0.0");

            var firmVersionProviderMock = new Mock<IFirmwareVersionProvider>();
            firmVersionProviderMock.Setup(s => s.GetSpecificFwAsync(commercialReference, version))
            .Throws(new NoRecordFoundException(Constants.Specific_FW_Not_Found_Message))
            .Verifiable();

            _bslclient.Setup(s => s.GetDocsByCR(commercialReference))
                .ReturnsAsync(new List<Document>());

            _utilityHelper.Setup(s => s.ParseVersion(It.IsAny<string>())).Returns(new Version("1.0.0"));


            //Act
            var act = () => _firmwareVersionProvider.GetSpecificFwAsync(commercialReference, version);

            //Assert
            NoRecordFoundException exception = await Assert.ThrowsAsync<NoRecordFoundException>(act);
            //Assert
            Assert.Equal(Constants.Specific_FW_Not_Found_Message, exception.Message);
        }

        [Fact]
        public async void GetFirmwareList_NotFound()
        {

            // Arrange
            string commercialReference = "Test123";

            var firmVersionProviderMock = new Mock<IFirmwareVersionProvider>();
            firmVersionProviderMock.Setup(s => s.GetFirmwareListAsync(commercialReference))
            .Throws(new NoRecordFoundException(Constants.FW_Files_Not_Found_Message))
            .Verifiable();

            _bslclient.Setup(s => s.GetDocsByCR(commercialReference))
                .ReturnsAsync(new List<Document>());

            _utilityHelper.Setup(s => s.ParseVersion(It.IsAny<string>())).Returns(new Version("1.0.0"));


            //Act
            var act = () => _firmwareVersionProvider.GetFirmwareListAsync(commercialReference);

            //Assert
            NoRecordFoundException exception = await Assert.ThrowsAsync<NoRecordFoundException>(act);
            //Assert
            Assert.Equal(Constants.FW_Files_Not_Found_Message, exception.Message);
        }
    }
}
