// Copyright Schneider-Electric 2024

using AutoMapper;
using ETPX.FirmwareRepository.Domain.Query;
using ETPX.FirmwareRepository.Entities;
using ETPX.FirmwareRepository.Exceptions;
using ETPX.FirmwareRepository.Helpers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ETPX.FirmwareRepository.Controllers
{
    /// <summary>
    /// The Firmware Repository Service.
    /// </summary>
    [Route("FirmwareRepository")]
    [ApiController]

    public class FirmwareRepositoryController : ControllerBase
    {
        private readonly ILogger<FirmwareRepositoryController> _logger;
        private readonly IMapper _mapper;
        private readonly ISender _sender;
        private readonly IValidator<string> _validatorCommercialReferenceRequest;

        /// <summary>
        /// FirmwareRepositoryController Constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="sender"></param>
        /// <param name="validatorCommercialReferenceRequest"></param>
        public FirmwareRepositoryController(ILogger<FirmwareRepositoryController> logger, IMapper mapper, ISender sender, IValidator<string> validatorCommercialReferenceRequest)
        {
            _logger = logger;
            _mapper = mapper;
            _sender = sender;
            _validatorCommercialReferenceRequest = validatorCommercialReferenceRequest;
        }

        /// <summary>
        /// Get firmware list
        /// </summary>
        /// <remarks>
        /// This API retrieves a history of firmware versions for a given commercial reference.
        /// <br/><br/>API response: Returns list of firmware versions <br/>
        ///     <b>downloadUrl:</b> firmware file download link. <br/>
        ///     <b>checkSumSHA256:</b> SHA256 Hashcode to get integrity of downloaded file from download link.<br/>
        ///     <b>version:</b> Firmware version.<br/>
        ///     <b>description:</b> Description of firmware version.<br/>
        ///     <b>fileExtension:</b> Extension of firmware file.<br/>
        ///     <b>fileName:</b> Name of firmware file.<br/>
        ///     <b>lastModifiedDate:</b> Last modified date of firmware file in UTC format.<br/>
        /// </remarks>
        /// <param name="commercialReference">The commercial reference for which to retrieve the firmware version <br/> Example: PAS600</param>
        /// <returns>GetFirmwareListResponse</returns>
        /// <response code="200">Successful operation. Returns the list of firmware versions</response>
        /// <response code="400">Firmware versions not found due to invalid commercial reference or non-availability of files</response>
        /// <response code="404">Invalid request. Commercial reference or firmware version is missing or invalid</response>
        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(GetFirmwareListResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 404)]

        public async Task<ActionResult> GetFirmwareList([Required]string commercialReference)
        {
            FluentValidation.Results.ValidationResult result = await _validatorCommercialReferenceRequest.ValidateAsync(commercialReference);
            if (!result.IsValid)
            {
                ControllerHelper.LogValidationErrorMessages(_logger, nameof(GetFirmwareList), new List<string> { $"Invalid input {commercialReference} for commercial Reference" });
                return BadRequest(ControllerHelper.LogInValidInputMessage(_logger, nameof(GetFirmwareList), EntityType.CommercialReferenceNo));
            }

            try
            {
                var response = await _sender.Send(new GetFirmwareListQuery(commercialReference));
                if (response != null)
                {
                    return Ok(_mapper.Map<GetFirmwareListResponse>(response));
                }
                return NotFound(ControllerHelper.LogFirmwareNotFoundMessage(_logger, nameof(GetFirmwareList), EntityType.CommercialReferenceNo, Constants.FW_Files_Not_Found_Message));
            }
            catch (NoRecordFoundException)
            {
                return NotFound(ControllerHelper.LogFirmwareNotFoundMessage(_logger, nameof(GetFirmwareList), EntityType.CommercialReferenceNo, Constants.FW_Files_Not_Found_Message));
            }
        }

        /// <summary>
        /// Get latest firmware
        /// </summary>
        /// <remarks>
        /// This API retrieves the latest firmware version files details for a given commercial reference and firmware version.<br/> Firmware version parameter is optional. If firmware version is provided then API gets the latest version higher than provided one. If not given it gets the latest version.
        /// <br/><br/>API response: <br/>
        ///     <b>downloadUrl:</b> Download link of latest firmware file <br/>
        ///     <b>checkSumSHA256:</b> SHA256 Hashcode to check integrity of downloaded file from download link.<br/>
        ///     <b>version:</b> Firmware version.<br/>
        ///     <b>description:</b> Description of firmware version.<br/>
        ///     <b>fileExtension:</b> Extension of firmware file.<br/>
        ///     <b>fileName:</b> Name of firmware file.<br/>
        ///     <b>lastModifiedDate:</b> Last modified date of firmware file in UTC format.<br/>
        /// </remarks>
        /// <param name="commercialReference">The commercial reference for which to retrieve the latest firmware version <br/> Example: PAS600</param>
        /// <param name="firmwareVersion">(Optional) The firmware version installed in device. If not provided, the latest firmware version will be returned <br/> Example: 1.0.0</param>
        /// <returns>GetFirmwareListResponse</returns>
        /// <response code="200">Successful operation. Returns the latest firmware version details</response>
        /// <response code="400">Invalid request. Commercial reference or firmware version is missing or invalid.</response>
        /// <response code="404">Latest firmware version not found due to invalid commercial reference or non-availability of files</response>
        [HttpGet("latestVersion")]
        [ProducesResponseType(typeof(GetFirmwareListResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 404)]

        public async Task<ActionResult> GetLatestFw([Required]string commercialReference, string? firmwareVersion ="")
        {
            FluentValidation.Results.ValidationResult result = await _validatorCommercialReferenceRequest.ValidateAsync(commercialReference);
            if (!result.IsValid)
            {
                ControllerHelper.LogValidationErrorMessages(_logger, nameof(GetLatestFw), new List<string> { $"Invalid input {commercialReference} for commercial Reference" });
                return BadRequest(ControllerHelper.LogInValidInputMessage(_logger, nameof(GetLatestFw), EntityType.CommercialReferenceNo));
            }

            Version? fwVersion = null;
            firmwareVersion = firmwareVersion?.Replace("{", string.Empty).Replace("}", string.Empty);
            if (firmwareVersion != null && !string.IsNullOrWhiteSpace(firmwareVersion) &&
                !Version.TryParse(firmwareVersion.ToLowerInvariant().Replace("version", string.Empty).Replace("v", string.Empty).Replace("_", "."), out fwVersion))
            {
                return BadRequest(ControllerHelper.LogInValidInputMessage(_logger, nameof(GetLatestFw), EntityType.FirmwareVersion));
            }
            try
            {
                var file = await _sender.Send(new GetLatestFirmwareFileQuery(commercialReference, fwVersion));
                if (file != null)
                {

                    _logger.LogInformation("Latest firmware version:{version} is fetched for given {commericalReference} successfully.", file.FirmwareList?.FirstOrDefault()?.Version, commercialReference);
                    return Ok(_mapper.Map<GetFirmwareListResponse>(file));
                }
                return NotFound(ControllerHelper.LogFirmwareNotFoundMessage(_logger, nameof(GetLatestFw), EntityType.CommercialReferenceNo, Constants.Latest_FW_Not_Found_Message));
            }
            catch (NoRecordFoundException)
            {
                return NotFound(ControllerHelper.LogFirmwareNotFoundMessage(_logger, nameof(GetLatestFw), EntityType.CommercialReferenceNo, Constants.Latest_FW_Not_Found_Message));
            }
        }

        /// <summary>
        /// Get specific firmware
        /// </summary>
        /// <remarks>
        /// This API retrieves a specific firmware version details for a given commercial reference and firmware version.<br/> Specific version can be used when current firmware version is not compatible with latest version. It requires device to upgrade to intermediate or specific version first then upgrade to latest version
        /// <br/><br/>API response: <br/>
        ///     <b>downloadUrl:</b> Download link of latest firmware file <br/>
        ///     <b>checkSumSHA256:</b> SHA256 Hashcode to check integrity of downloaded file from download link.<br/>
        ///     <b>version:</b> Firmware version.<br/>
        ///     <b>description:</b> Description of firmware version.<br/>
        ///     <b>fileExtension:</b> Extension of firmware file.<br/>
        ///     <b>fileName:</b> Name of firmware file.<br/>
        ///     <b>lastModifiedDate:</b> Last modified date of firmware file in UTC format.<br/>
        /// </remarks>
        /// <param name="commercialReference">The commercial reference for which to retrieve the specific firmware version <br/> Example: PAS600</param>
        /// <param name="firmwareVersion">The specific firmware version to retrieve <br/> Example: 1.0.0</param>
        /// <returns>GetFirmwareListResponse</returns>
        /// <response code="200">Successful operation. Returns the specific firmware version details </response>
        /// <response code="400">Invalid request. Commercial reference or firmware is missing or invalid.</response>
        /// <response code="404">Latest firmware version not found due to invalid commercial reference or non-availability of files</response>
        [HttpGet]
        [Route("specificVersion")]
        [ProducesResponseType(typeof(GetFirmwareListResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult> GetSpecificFw([Required] string commercialReference, [Required] string firmwareVersion)
        {
            FluentValidation.Results.ValidationResult result = await _validatorCommercialReferenceRequest.ValidateAsync(commercialReference);
            if (!result.IsValid)
            {
                ControllerHelper.LogValidationErrorMessages(_logger, nameof(GetSpecificFw), new List<string> { $"Invalid input {commercialReference} for commercial Reference" });
                return BadRequest(ControllerHelper.LogInValidInputMessage(_logger, nameof(GetSpecificFw), EntityType.CommercialReferenceNo));
            }
            if (string.IsNullOrEmpty(firmwareVersion))
            {
                return BadRequest(ControllerHelper.LogInValidInputMessage(_logger, nameof(GetSpecificFw), EntityType.FirmwareVersion));
            }
            Version? fwVersion;
            if (!Version.TryParse(firmwareVersion.ToLowerInvariant().Replace("version", string.Empty).Replace("v", string.Empty).Replace("_", "."), out fwVersion))
            {
                ControllerHelper.LogValidationErrorMessages(_logger, nameof(GetSpecificFw), new List<string> { $"Invalid input {firmwareVersion} for Firmware Version" });
                return BadRequest(ControllerHelper.LogInValidInputMessage(_logger, nameof(GetSpecificFw), EntityType.FirmwareVersion));
            }
            try
            {
                var file = await _sender.Send(new GetSpecificFirmwareVersionQuery(commercialReference, fwVersion));
                if (file != null)
                {
                    _logger.LogInformation("Specific firmware version:{version} is fetched for given {commericalReference} successfully.", fwVersion, commercialReference);
                    return Ok(_mapper.Map<GetFirmwareListResponse>(file));
                }
                return NotFound(ControllerHelper.LogFirmwareNotFoundMessage(_logger, nameof(GetSpecificFw), EntityType.CommercialReferenceNo, Constants.Specific_FW_Not_Found_Message));
            }
            catch (NoRecordFoundException)
            {
                return NotFound(ControllerHelper.LogFirmwareNotFoundMessage(_logger, nameof(GetSpecificFw), EntityType.CommercialReferenceNo, Constants.Specific_FW_Not_Found_Message));
            }
        }
    }
}