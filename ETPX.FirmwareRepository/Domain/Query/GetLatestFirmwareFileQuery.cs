// Copyright Schneider-Electric 2024

﻿using ETPX.FirmwareRepository.BSL;
using ETPX.FirmwareRepository.Entities;
using ETPX.FirmwareRepository.ServiceProvider;
using MediatR;
using RestSharp;
using System.Runtime.InteropServices;

namespace ETPX.FirmwareRepository.Domain.Query
{
    /// <summary>
    /// Query to get latest firmware for given commercial reference and firmware version.
    /// </summary>
    /// <param name="commercialReference"></param>
    /// <param name="fwVersion"></param>
    public record GetLatestFirmwareFileQuery(string commercialReference, Version? fwVersion) :  IRequest<GetFirmwareListResponse>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public class GetLatestFirmwareFileQueryHandler : IRequestHandler<GetLatestFirmwareFileQuery, GetFirmwareListResponse>
        {
            private readonly IFirmwareVersionProvider firmwareVersionProvider;
         
            /// <summary>
            /// Query Handler
            /// </summary>
            /// <param name="firmwareVersionProvider"></param>
            public GetLatestFirmwareFileQueryHandler(IFirmwareVersionProvider firmwareVersionProvider)
            {
                this.firmwareVersionProvider = firmwareVersionProvider;
            }
            /// <summary>
            /// Handles Request
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<GetFirmwareListResponse> Handle(GetLatestFirmwareFileQuery request, CancellationToken cancellationToken)
            {
                var result = await firmwareVersionProvider.GetLatestFirmwareAsync(request.commercialReference, request.fwVersion);
                return new GetFirmwareListResponse { FirmwareList = result };
            }
        }
    }

   
}
