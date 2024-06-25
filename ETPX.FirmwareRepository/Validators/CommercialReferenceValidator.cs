// Copyright Schneider-Electric 2024

﻿using ETPX.FirmwareRepository.Entities;
using FluentValidation;

namespace ETPX.FirmwareRepository.Validators
{
    /// <summary>
    /// Commerical Reference Validator
    /// </summary>
    public class CommericalReferenceValidator : AbstractValidator<string>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CommericalReferenceValidator()
        {
            RuleFor(model => model)
            .NotEmpty()
            .Matches(Constants.Regex_CommercialReference)
            .MaximumLength(15)
            .MinimumLength(3)
            .When(i => !string.IsNullOrWhiteSpace(i), ApplyConditionTo.CurrentValidator); 
        }
    }
}
