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
    /// Query to Get Specific firmware version
    /// </summary>
    /// <param name="commercialReference"></param>
    /// <param name="fwVersion"></param>
    public record GetSpecificFirmwareVersionQuery(string commercialReference, Version fwVersion) :  IRequest<GetFirmwareListResponse>
    {
        /// <summary>
        /// Query Handler.
        /// </summary>
        public class GetSpecificFirmwareVersionQueryHandler : IRequestHandler<GetSpecificFirmwareVersionQuery, GetFirmwareListResponse>
        {
            private readonly IFirmwareVersionProvider firmwareVersionProvider;
          
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="firmwareVersionProvider"></param>
            public GetSpecificFirmwareVersionQueryHandler(IFirmwareVersionProvider firmwareVersionProvider)
            {
                this.firmwareVersionProvider = firmwareVersionProvider;
            }

            /// <summary>
            /// Handles request
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<GetFirmwareListResponse> Handle(GetSpecificFirmwareVersionQuery request, CancellationToken cancellationToken)
            {
                var result = await firmwareVersionProvider.GetSpecificFwAsync(request.commercialReference, request.fwVersion);
                return new GetFirmwareListResponse { FirmwareList = result };
            }
        }
    }
}
