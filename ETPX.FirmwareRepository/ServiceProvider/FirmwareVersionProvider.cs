// Copyright Schneider-Electric 2024

﻿using ETPX.FirmwareRepository.BSL;
using ETPX.FirmwareRepository.Domain.Entities;
using ETPX.FirmwareRepository.Entities;
using ETPX.FirmwareRepository.Exceptions;
using ETPX.FirmwareRepository.Helpers;
using ETPX.FirmwareRepository.Settings;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;

namespace ETPX.FirmwareRepository.ServiceProvider
{
    /// <summary>
    /// Firmware version provider
    /// </summary>
    public class FirmwareVersionProvider : IFirmwareVersionProvider
    {
        private readonly BslClient _bslclient;
        private readonly UtilityHelper _utilityHelper;
        private readonly IBslSettings _settings;

        /// <summary>
        /// FirmwareVersionProvider Constructor
        /// </summary>
        /// <param name="bslClient">BSL client</param>
        /// <param name="utilityHelper">Utility Helper</param>
        /// <param name="bslSettings">BSL Settings</param>
        public FirmwareVersionProvider(BslClient bslClient, UtilityHelper utilityHelper, IBslSettings bslSettings) {
            _bslclient = bslClient;
            _utilityHelper = utilityHelper;
            _settings = bslSettings;
        }
        /// <summary>
        /// Gets list of Firmware files for given commercial reference.
        /// </summary>
        /// <param name="commercialReference">Commercial Reference Number</param>
        /// <returns>List of Firmware details</returns>
        public async Task<List<FirmwareDetail>> GetFirmwareListAsync(string commercialReference)
        {
            List<FirmwareDetail> serviceResponse = new List<FirmwareDetail>();
            var docs = await GetFirmwareDocuments(commercialReference);
            foreach (var doc in docs.OrderByDescending(x => _utilityHelper.ParseVersion(x.Revision)).OrderBy(y => y.CreationDate))
            {
                foreach (var fwfile in doc.Files.File)
                {
                    string downloadLink = string.IsNullOrEmpty(fwfile.DownloadLink) ? string.Format(_settings.FileDownloadBaseUrl + Constants.FW_Download_link_EndPoint, fwfile.Filename, doc.Reference) : fwfile.DownloadLink;
                    serviceResponse.Add(
                    new FirmwareDetail
                    {
                        DownloadUrl = downloadLink,
                        CheckSumSHA256 = fwfile.Sha256,
                        Description = doc.Title,
                        Version = doc.Revision,
                        FileExtension = fwfile.Extension,
                        FileName = fwfile.Filename,
                        LastModifiedDate = doc.LastModificationDate
                    });
                }
            }
            if(serviceResponse.Count == 0)
            {
                throw new NoRecordFoundException(Constants.FW_Files_Not_Found_Message);
            }
            return serviceResponse;
        }

        /// <summary>
        /// Gets the latest Firmware details for given commercial reference and firmware Version.
        /// </summary>
        /// <param name="commercialReference">Commercial Reference Number</param>
        /// <param name="fwVersion">Optional Firmware version</param>
        /// <returns>Latest Firmware details</returns>
        public async Task<List<FirmwareDetail>> GetLatestFirmwareAsync(string commercialReference, Version? fwVersion)
        {
            List<FirmwareDetail> serviceResponse = new List<FirmwareDetail>();
            var latestdocs = await GetLatestDocuments(commercialReference, fwVersion) ?? throw new NoRecordFoundException(Constants.Latest_FW_Not_Found_Message);
            foreach (var doc in latestdocs.OrderByDescending(x => _utilityHelper.ParseVersion(x.Revision)).OrderBy(y => y.CreationDate))
            {
                foreach (var fwfile in doc.Files.File)
                {
                    string downloadLink = string.IsNullOrEmpty(fwfile.DownloadLink) ? string.Format(_settings.FileDownloadBaseUrl + Constants.FW_Download_link_EndPoint, fwfile.Filename, doc.Reference) : fwfile.DownloadLink;
                    serviceResponse.Add(
                    new FirmwareDetail
                    {
                        DownloadUrl = downloadLink,
                        CheckSumSHA256 = fwfile.Sha256,
                        Description = doc.Title,
                        Version = doc.Revision,
                        FileExtension = fwfile.Extension,
                        FileName = fwfile.Filename,
                        LastModifiedDate = doc.LastModificationDate
                    });
                }
            }
            if (serviceResponse.Count == 0)
            {
                throw new NoRecordFoundException(Constants.Latest_FW_Not_Found_Message);
            }
            return serviceResponse;
        }

        /// <summary>
        /// Gets the specific Firmware details for given commercial reference and firmware Version.
        /// </summary>
        /// <param name="commercialReference">Commercial Reference Number</param>
        /// <param name="fwVersion">Firmware version</param>
        /// <returns>list of FirmwareDetail </returns>
        public async Task<List<FirmwareDetail>> GetSpecificFwAsync(string commercialReference, Version fwVersion)
        {
            List<FirmwareDetail> serviceResponse = new List<FirmwareDetail>();
            var docs = await GetFirmwareDocuments(commercialReference);
            var docsWithVersion = docs.Select(x => new { version = _utilityHelper.ParseVersion(x.Revision), doc = x }).ToList();
            var matchedDocs = docsWithVersion.Where(x => x.version.Equals(fwVersion));
            var specificDocs = matchedDocs.Select(x => x.doc).ToList();

            foreach (var doc in specificDocs.OrderByDescending(x => _utilityHelper.ParseVersion(x.Revision)).OrderBy(y => y.CreationDate))
            {
                foreach (var fwfile in doc.Files.File)
                {
                    string downloadLink = string.IsNullOrEmpty(fwfile.DownloadLink) ? string.Format(_settings.FileDownloadBaseUrl + Constants.FW_Download_link_EndPoint, fwfile.Filename, doc.Reference) : fwfile.DownloadLink;
                    serviceResponse.Add(
                    new FirmwareDetail
                    {
                        DownloadUrl = downloadLink,
                        CheckSumSHA256 = fwfile.Sha256,
                        Description = doc.Title,
                        Version = doc.Revision,
                        FileExtension = fwfile.Extension,
                        FileName = fwfile.Filename,
                        LastModifiedDate = doc.LastModificationDate
                    });
                }
            }
            if (serviceResponse.Count == 0)
            {
                throw new NoRecordFoundException(Constants.Specific_FW_Not_Found_Message);
            }
            return serviceResponse;
        }


        private async Task<List<Document>> GetLatestDocuments(string commercialReference, Version? fwVersion)
        {
            List<Document> latestdocs = new List<Document>();
            var documents = await GetFirmwareDocuments(commercialReference);
            var docsWithVersion = documents.Select(x => new { version = _utilityHelper.ParseVersion(x.Revision), doc = x });
            if (fwVersion != null)
            {
                var docs = docsWithVersion.Where(x => x.version.CompareTo(fwVersion) >= 0);
                var latestVersion = docs.Max(x => x.version);
                latestdocs = docs.Where(x => x.version == latestVersion).Select(x => x.doc).ToList();
            }
            else
            {
                var docs = docsWithVersion.OrderByDescending(x => x.version);
                var latestVersion = docs.Max(x => x.version);
                latestdocs = docs.Where(x => x.version == latestVersion).Select(x => x.doc).ToList();
            }
            return latestdocs;
        }

        private async Task<IEnumerable<Document>> GetFirmwareDocuments(string commercialReference)
        {
            var docs = await _bslclient.GetDocsByCR(commercialReference);
            var filteredDocs  = new List<Document>();
            foreach (var doc in docs.Where(doc => doc.Files.File.Exists(x => Constants.SupportedFirmwareExtension.Exists(y => y.ToLower() == x.Extension.ToLower()))))
            {
                var filteredFiles = doc.Files.File.Where(x => Constants.SupportedFirmwareExtension.Exists(y => y.ToLower() == x.Extension.ToLower()));
                doc.Files.File = filteredFiles.ToList();
                filteredDocs.Add(doc);
            }
            return filteredDocs;
        }
    }
}
