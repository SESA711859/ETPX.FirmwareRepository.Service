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
    /// Query to get firmware version list
    /// </summary>
    /// <param name="commercialReference"></param>
    public record GetFirmwareListQuery(string commercialReference) :  IRequest<GetFirmwareListResponse>
    {

        /// <summary>
        /// Query Handler
        /// </summary>
        public class GetFirmwareListQueryHandler : IRequestHandler<GetFirmwareListQuery, GetFirmwareListResponse>
        {
            private readonly IFirmwareVersionProvider firmwareVersionProvider;
           
            /// <summary>
           /// Constructor
           /// </summary>
           /// <param name="firmwareVersionProvider"></param>
            public GetFirmwareListQueryHandler(IFirmwareVersionProvider firmwareVersionProvider)
            {
                this.firmwareVersionProvider = firmwareVersionProvider;
            }

            /// <summary>
            /// Handler implementation
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<GetFirmwareListResponse> Handle(GetFirmwareListQuery request, CancellationToken cancellationToken)
            {
                var result = await firmwareVersionProvider.GetFirmwareListAsync(request.commercialReference);
                return new GetFirmwareListResponse { FirmwareList = result };
            }
        }
    }
}
