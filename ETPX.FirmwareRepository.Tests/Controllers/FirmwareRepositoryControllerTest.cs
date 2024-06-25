// Copyright Schneider-Electric 2024

﻿using AutoFixture;
using AutoMapper;
using ETPX.FirmwareRepository.Controllers;
using ETPX.FirmwareRepository.Domain.Query;
using ETPX.FirmwareRepository.Entities;
using ETPX.FirmwareRepository.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ETPX.FirmwareRepository.Tests.Controllers
{

    public class FirmwareRepositoryControllerTest
    {
        private readonly Mock<ISender> _sender;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<FirmwareRepositoryController>> _logger;
        private readonly FirmwareRepositoryController _controller;
        private readonly IFixture fixture;
        private readonly Mock<IValidator<string>> _validator;
        public FirmwareRepositoryControllerTest()
        {
            fixture = new Fixture();

            _sender = new Mock<ISender>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<FirmwareRepositoryController>>();
            _validator = fixture.Freeze<Mock<IValidator<string>>>();
            _controller = new FirmwareRepositoryController(_logger.Object, _mapper.Object, _sender.Object, _validator.Object);
        }

        [Fact]
        public void GetFirmwareList_Ok()
        {
            // Arrange
            string commercialReference = "PAS600";
            _sender
            .Setup(m => m.Send(It.IsAny<GetFirmwareListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetFirmwareListResponse()
            {
                FirmwareList = new List<FirmwareDetail> { new FirmwareDetail
                    {
                          DownloadUrl ="http://abc.com",
                          Description ="Test",
                          FileExtension =".sedp",
                          LastModifiedDate = DateTime.Now,
                          FileName = "Test.sedp",
                          Version = "1.0.0",
                          CheckSumSHA256 = "343434343434343"
                    }
                }
            })
            .Verifiable();
            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult()));

            //Act
            var result = _controller.GetFirmwareList(commercialReference);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okObjectResult.StatusCode);
        }

        [Fact]
        public void GetFirmwareList_BadRequest()
        {
            // Arrange
            string commercialReference = string.Empty;
            _sender
            .Setup(m => m.Send(It.IsAny<GetFirmwareListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetFirmwareListResponse()
            {
                FirmwareList = new List<FirmwareDetail> { new FirmwareDetail
                    {
                          DownloadUrl ="http://abc.com",
                          Description ="Test",
                          FileExtension =".sedp",
                          LastModifiedDate = DateTime.Now,
                          FileName = "Test.sedp",
                          Version = "1.0.0",
                          CheckSumSHA256 = "343434343434343"
                    }
                }
            })
            .Verifiable();
            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult(new[] { new ValidationFailure("Property", "'PropetyName'") })));

            //Act
            var result = _controller.GetFirmwareList(commercialReference);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void GetFirmwareList_Not_Found()
        {
            // Arrange
            string commercialReference = "ABC";
            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult()));

            //Act
            var result = _controller.GetFirmwareList(commercialReference);

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, notFoundObjectResult.StatusCode);
        }

        [Fact]
        public void GetFirmwareList_Invalid_CommercialReference()
        {
            // Arrange
            string commercialReference = "ABC%#";

            var validationErrors = new List<ValidationFailure>
            {
                new ValidationFailure("commercialReference", "Invalid Commercial Reference No")
            };

            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult(validationErrors)));

            //Act
            var result = _controller.GetFirmwareList(commercialReference);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void Get_LatestFirmware_By_commercialReference_FirmwareVersion_Ok()
        {
            // Arrange
            string commercialReference = "PAS600";
            string firmwareVersion = "1.0.0";
            _sender
            .Setup(m => m.Send(It.IsAny<GetLatestFirmwareFileQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetFirmwareListResponse
            {
                FirmwareList = new List<FirmwareDetail>()
                { 
                    new FirmwareDetail
                    {
                        DownloadUrl = "http://abc.com",
                        Description = "Test",
                        FileExtension = ".sedp",
                        LastModifiedDate = DateTime.Now,
                        FileName = "Test.sedp",
                        Version = "2.0.0",
                        CheckSumSHA256 = "343434343434343"
                    } 
                }
            })
            .Verifiable();

            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult()));

            //Act
            var result = _controller.GetLatestFw(commercialReference, firmwareVersion);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okObjectResult.StatusCode);
        }

        [Fact]
        public void Get_LatestFirmware_By_commercialReference_FirmwareVersion_BadRequest()
        {
            // Arrange
            string commercialReference = string.Empty;
            string firmwareVersion = "1.0.0";
            _sender
            .Setup(m => m.Send(It.IsAny<GetLatestFirmwareFileQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetFirmwareListResponse
            {
                FirmwareList = new List<FirmwareDetail>()
                {
                    new FirmwareDetail
                    {
                        DownloadUrl = "http://abc.com",
                        Description = "Test",
                        FileExtension = ".sedp",
                        LastModifiedDate = DateTime.Now,
                        FileName = "Test.sedp",
                        Version = "2.0.0",
                        CheckSumSHA256 = "343434343434343"
                    }
                }
            })
            .Verifiable();

            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult(new[] { new ValidationFailure("Property", "'PropetyName'") })));

            //Act
            var result = _controller.GetLatestFw(commercialReference, firmwareVersion);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void Get_LatestFirmware_By_commercialReference_FirmwareVersion_NotFound()
        {
            // Arrange
            string commercialReference = "ABDC";
            string firmwareVersion = "1.0.0";

            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult()));

            //Act
            var result = _controller.GetLatestFw(commercialReference, firmwareVersion);

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, notFoundObjectResult.StatusCode);
        }

        [Fact]
        public void Get_LatestFirmware_Invalid_CommercialReference()
        {
            // Arrange
            string commercialReference = "ABC%#";
            string firmwareVersion = "1.0.0";

            var validationErrors = new List<ValidationFailure>
            {
                new ValidationFailure("commercialReference", "Invalid Commercial Reference No")
            };

            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult(validationErrors)));

            //Act
            var result = _controller.GetLatestFw(commercialReference, firmwareVersion);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void Get_LatestFirmwareVersion_By_commercialReference_Without_FirmwareVersion_Ok()
        {
            // Arrange
            string commercialReference = "PAS600";
            _sender
            .Setup(m => m.Send(It.IsAny<GetLatestFirmwareFileQuery>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync(new GetFirmwareListResponse
             {
                 FirmwareList = new List<FirmwareDetail>()
                {
                    new FirmwareDetail
                    {
                        DownloadUrl = "http://abc.com",
                        Description = "Test",
                        FileExtension = ".sedp",
                        LastModifiedDate = DateTime.Now,
                        FileName = "Test.sedp",
                        Version = "2.0.0",
                        CheckSumSHA256 = "343434343434343"
                    }
                }
             })
            .Verifiable();
            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult()));

            //Act
            var result = _controller.GetLatestFw(commercialReference);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okObjectResult.StatusCode);
        }

        [Fact]
        public void Get_LatestFirmware_Throw_NoRecordFoundException()
        {
            // Arrange
            string commercialReference = "PAS600";
            string firmwareVersion = "1.0.0";
            _sender
            .Setup(m => m.Send(It.IsAny<GetLatestFirmwareFileQuery>(), It.IsAny<CancellationToken>()))
            .Throws(new NoRecordFoundException("No record found"));
            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult()));

            //Act
            var result = _controller.GetLatestFw(commercialReference, firmwareVersion);

            //Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, notFoundObjectResult.StatusCode);
        }


        [Fact]
        public void Get_Specific_Firmware_Version_Ok()
        {
            // Arrange
            string commercialReference = "PAS600";
            string firmwareVersion = "1.0.0";
            _sender
            .Setup(m => m.Send(It.IsAny<GetSpecificFirmwareVersionQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetFirmwareListResponse
            {
                FirmwareList = new List<FirmwareDetail>()
                {
                    new FirmwareDetail
                    {
                        DownloadUrl = "http://abc.com",
                        Description = "Test",
                        FileExtension = ".sedp",
                        LastModifiedDate = DateTime.Now,
                        FileName = "Test.sedp",
                        Version = "2.0.0",
                        CheckSumSHA256 = "343434343434343"
                    }
                }
            })
            .Verifiable();
            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult()));

            //Act
            var result = _controller.GetSpecificFw(commercialReference, firmwareVersion);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okObjectResult.StatusCode);
        }

        [Fact]
        public void Get_Specific_Firmware_Version_BadRequest()
        {
            // Arrange
            string commercialReference = string.Empty;
            string firmwareVersion = "1.0.0";
            _sender
            .Setup(m => m.Send(It.IsAny<GetSpecificFirmwareVersionQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetFirmwareListResponse
             {
                 FirmwareList = new List<FirmwareDetail>()
                {
                    new FirmwareDetail
                    {
                        DownloadUrl = "http://abc.com",
                        Description = "Test",
                        FileExtension = ".sedp",
                        LastModifiedDate = DateTime.Now,
                        FileName = "Test.sedp",
                        Version = "2.0.0",
                        CheckSumSHA256 = "343434343434343"
                    }
                }
             })
            .Verifiable();
            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult(new[] { new ValidationFailure("Property", "'PropetyName'") })));

            //Act
            var result = _controller.GetSpecificFw(commercialReference, firmwareVersion);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void Get_Specific_Firmware_Version_Empty_Firmware_BadRequest()
        {
            // Arrange
            string commercialReference = "PAS600";
            string firmwareVersion = string.Empty;
            _sender
            .Setup(m => m.Send(It.IsAny<GetSpecificFirmwareVersionQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetFirmwareListResponse
             {
                 FirmwareList = new List<FirmwareDetail>()
                {
                    new FirmwareDetail
                    {
                        DownloadUrl = "http://abc.com",
                        Description = "Test",
                        FileExtension = ".sedp",
                        LastModifiedDate = DateTime.Now,
                        FileName = "Test.sedp",
                        Version = "2.0.0",
                        CheckSumSHA256 = "343434343434343"
                    }
                }
             })
            .Verifiable();
            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult()));

            //Act
            var result = _controller.GetSpecificFw(commercialReference, firmwareVersion);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void Get_Specific_Firmware_Version_Throw_NoRecordFoundException()
        {
            // Arrange
            string commercialReference = "PAS600";
            string firmwareVersion = "1.0.0";
            _sender
            .Setup(m => m.Send(It.IsAny<GetSpecificFirmwareVersionQuery>(), It.IsAny<CancellationToken>()))
            .Throws(new NoRecordFoundException("No record found"));
            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult()));

            //Act
            var result = _controller.GetSpecificFw(commercialReference, firmwareVersion);

            //Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, notFoundObjectResult.StatusCode);

        }

        [Fact]
        public void Get_Specific_Firmware_Version_NotFound()
        {
            // Arrange
            string commercialReference = "ABDC";
            string firmwareVersion = "1.0.0";
            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult()));

            //Act
            var result = _controller.GetSpecificFw(commercialReference, firmwareVersion);

            //Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public void Get_Specific_Firmware_Version_Invalid_CommercialReference()
        {
            // Arrange
            string commercialReference = "ABC%#";
            string firmwareVersion = "1.0.0";

            var validationErrors = new List<ValidationFailure>
            {
                new ValidationFailure("commercialReference", "Invalid Commercial Reference No")
            };

            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult(validationErrors)));

            //Act
            var result = _controller.GetSpecificFw(commercialReference, firmwareVersion);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void Get_Specific_Firmware_Version_Wrong_FirmwareVersion_BadRequest()
        {
            // Arrange
            string commercialReference = "PAS600";
            string firmwareVersion = "ABC";
            _sender
            .Setup(m => m.Send(It.IsAny<GetSpecificFirmwareVersionQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetFirmwareListResponse
             {
                 FirmwareList = new List<FirmwareDetail>()
                {
                    new FirmwareDetail
                    {
                        DownloadUrl = "http://abc.com",
                        Description = "Test",
                        FileExtension = ".sedp",
                        LastModifiedDate = DateTime.Now,
                        FileName = "Test.sedp",
                        Version = "2.0.0",
                        CheckSumSHA256 = "343434343434343"
                    }
                }
             })
            .Verifiable();
            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult()));

            //Act
            var result = _controller.GetSpecificFw(commercialReference, firmwareVersion);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void Get_LatestFirmware_By_commercialReference_Wrong_FirmwareVersion_BadRequest()
        {
            // Arrange
            string commercialReference = "PAS600";
            string firmwareVersion = "ABC";
            _sender
            .Setup(m => m.Send(It.IsAny<GetLatestFirmwareFileQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetFirmwareListResponse
             {
                 FirmwareList = new List<FirmwareDetail>()
                {
                    new FirmwareDetail
                    {
                        DownloadUrl = "http://abc.com",
                        Description = "Test",
                        FileExtension = ".sedp",
                        LastModifiedDate = DateTime.Now,
                        FileName = "Test.sedp",
                        Version = "2.0.0",
                        CheckSumSHA256 = "343434343434343"
                    }
                }
             })
            .Verifiable();
            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult()));

            //Act
            var result = _controller.GetLatestFw(commercialReference, firmwareVersion);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void Get_FirmwareList_Throw_NoRecordFoundException()
        {
            // Arrange
            string commercialReference = "ABC";
            _sender
            .Setup(m => m.Send(It.IsAny<GetFirmwareListQuery>(), It.IsAny<CancellationToken>()))
            .Throws(new NoRecordFoundException("No record found"));
            _validator.Setup(h => h.ValidateAsync(commercialReference, default)).Returns(Task.FromResult(new ValidationResult()));

            //Act
            var result = _controller.GetFirmwareList(commercialReference);

            //Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, notFoundObjectResult.StatusCode);

        }
    }
}