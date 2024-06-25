// Copyright Schneider-Electric 2024

using AutoFixture;
using ETPX.FirmwareRepository.Validators;
using FluentAssertions;

namespace ETPX.FirmwareRepository.Tests.Validators
{
    public class CommercialReferenceValidatorTest
    {
        private Fixture _autofixture;

        public CommercialReferenceValidatorTest()
        {
            _autofixture = new Fixture();
        }

        private CommericalReferenceValidator CreateCommercialReferenceValidator()
        {
            return new CommericalReferenceValidator();
        }

        [Fact]
        public void Validate_WhenInvalidStatus_ShouldHaveValidationError()
        {
            // Arrange
            var validator = this.CreateCommercialReferenceValidator();
            string commercialReference = "PAS600%%";

            // Act
            var result = validator.Validate(commercialReference);

            // Assert
            result.IsValid.Should().BeFalse();
        }
    }
}
