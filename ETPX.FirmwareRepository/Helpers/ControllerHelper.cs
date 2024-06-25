// Copyright Schneider-Electric 2024

﻿using ETPX.FirmwareRepository.Entities;

namespace ETPX.FirmwareRepository.Helpers
{
    /// <summary>
    /// Controller helpers method
    /// </summary>
    public static class ControllerHelper
    {
        /// <summary>
        /// Log NotFound warning and returns response message
        /// </summary>
        /// <param name="_logger"></param>
        /// <param name="methodName"></param>
        /// <param name="entity"></param>
        /// <param name="entityValue"></param>
        public static ApiResponse LogNotFoundMessage(ILogger _logger, string methodName, EntityType entity, string? entityValue = null)
        {
            _logger.LogWarning("{MethodName} : {Entity} - {EntityValue} not found.", methodName, entity.ToString(), entityValue ?? string.Empty);
            return new ApiResponse($"{entity} not found.");
        }

        /// <summary>
        /// Log Mandatory Input warning and returns response message
        /// </summary>
        /// <param name="_logger"></param>
        /// <param name="methodName"></param>
        /// <param name="entity"></param>
        public static ApiResponse LogBadInputMessage(ILogger _logger, string methodName, EntityType entity)
        {
            _logger.LogWarning("{MethodName} : {Entity} is required.", methodName, entity.ToString());
            return new ApiResponse($"{entity} is invalid.", System.Net.HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Log InValid Input warning and returns response message
        /// </summary>
        /// <param name="_logger"></param>
        /// <param name="methodName"></param>
        /// <param name="entity"></param>
        public static ApiResponse LogInValidInputMessage(ILogger _logger, string methodName, EntityType entity)
        {
            _logger.LogWarning("{MethodName} : {Entity} is InValid.", methodName, entity.ToString());
            return new ApiResponse($"{entity} is invalid.", System.Net.HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Log Validation Error Messages and returns response message
        /// </summary>
        /// <param name="_logger"></param>
        /// <param name="methodName"></param>
        /// <param name="errorMessages"></param>
        public static ApiResponse LogValidationErrorMessages(ILogger _logger, string methodName, IEnumerable<string> errorMessages)
        {
            _logger.LogWarning("{MethodName} : Validation Failed, Error Messages : {Errors}.", methodName, string.Join(" || ", errorMessages));
            return new ApiResponse(errorMessages);
        }   

        /// <summary>
        /// Log Validation Error Messages
        /// </summary>
        /// <param name="_logger"></param>
        /// <param name="entity"></param>
        /// <param name="entityValue"></param>
        public static void LogCreateUpdateMessage(ILogger _logger, EntityType entity, string entityValue)
        {
            _logger.LogInformation("Created/Updated {Entity} - {Id} successfully.", entity.ToString(), entityValue);
        }

        /// <summary>
        /// Log Get Response
        /// </summary>
        /// <param name="_logger"></param>
        /// <param name="methodName"></param>
        /// <param name="entity"></param>
        /// <param name="entityValue"></param>
        public static void LogGetResponseMessage(ILogger _logger, string methodName, EntityType entity, string entityValue)
        {
            _logger.LogInformation("Returned {Call} response for {Entity} {Id} successfully.", methodName, entity, entityValue);
        }
        /// <summary>
        /// Log NotFound warning and returns response message
        /// </summary>
        /// <param name="_logger"></param>
        /// <param name="methodName"></param>
        /// <param name="entity"></param>
        /// <param name="entityValue"></param>
        /// <returns></returns>
        public static ApiResponse LogFirmwareNotFoundMessage(ILogger _logger, string methodName, EntityType entity, string? entityValue = null)
        {
            string message = entityValue ?? "Firmware version not found";
            _logger.LogWarning("{MethodName} : {Entity} - {EntityValue} not found.", methodName, entity.ToString(), message);
            return new ApiResponse(message);
        }
    }
}