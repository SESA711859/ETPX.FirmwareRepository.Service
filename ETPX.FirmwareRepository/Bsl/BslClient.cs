// Copyright Schneider-Electric 2024

﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using ETPX.FirmwareRepository.Entities;
using ETPX.FirmwareRepository.Domain.Entities;
using Newtonsoft.Json;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Reflection;
using RestSharp.Authenticators;
using ETPX.FirmwareRepository.Domain.Exceptions;
using ETPX.FirmwareRepository.BSL.Entities;
using ETPX.FirmwareRepository.Settings;
using Newtonsoft.Json.Serialization;

namespace ETPX.FirmwareRepository.BSL
{

    /// <summary>
    /// BSL Client
    /// </summary>
    public class BslClient
    {
        string BSL_API_TOKEN = string.Empty;
       
        private readonly string baseUrl;

        private readonly IBslSettings bslSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings"></param>
        public BslClient(IBslSettings settings)
        {
            this.bslSettings = settings;
            baseUrl = $"{this.bslSettings.BaseUrl}pim-document";
        }
        /// <summary>
        /// Get firmware docs from BSL
        /// </summary>
        /// <param name="commercialReference">Commercial reference number</param>
        /// <returns>List of firmware documents</returns>
       public async virtual Task<List<Document>> GetDocsByCR(string commercialReference)
        {
            var requestBody = new DocPagesSearchRequest
            {
                GetDocumentPage = new GetDocumentPage
                {
                    Scope = new Scope { Brand = "Schneider Electric", Country = "WW" },
                    Query = new Query
                    {
                        DocTypes = new List<string> { "1555893" },  //"1555899" - Software - Hotfix, 1555893 = firmware
                        ProductCommRef = new List<string> { commercialReference },
                        FileExtensions = Constants.SupportedFirmwareExtension.ToList(),
                    },
                    Version = 11,
                    Pagination = new Pagination { FirstResult = 0, MaxResult = 10 },
                }
            };
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var jsonRequest = JsonConvert.SerializeObject(requestBody);
            string resoponse = string.Empty;

            if (string.IsNullOrEmpty(BSL_API_TOKEN))
            {
                await SetBslApiToken();
            }
            try
            {
                resoponse = POST(baseUrl, BSL_API_TOKEN, Constants.BSL_DocRef_pages_search_endpoint, jsonRequest);
            }
            catch (TokenExpiredException)
            {
                await SetBslApiToken();
                resoponse = POST(baseUrl, BSL_API_TOKEN, Constants.BSL_DocRef_pages_search_endpoint, jsonRequest);
            }

            var docRefPageResponse = JsonConvert.DeserializeObject<DocRefPageResponse>(resoponse);
            var docs = docRefPageResponse?.GetDocumentPageResponse?.Return?.Documents?.Document;
            if(docs != null)
            {
                return docs;
            }
            return new List<Document>();
        }

        #region private methods


        private async Task SetBslApiToken()
        {
            var apitoken = await GetAppToken();
            if (apitoken != null)
            {
                BSL_API_TOKEN = apitoken.AccessToken;
            }
        }

        private async Task<BslTokenResponse?> GetAppToken()
        {
            var authenticationString = $"{bslSettings.ClientId}:{bslSettings.ClientSecret}";
            var token = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));
            token = $"Basic {token}";

            var options = new RestClientOptions(bslSettings.TokenUrl)
            {
                Authenticator = new HttpBasicAuthenticator(bslSettings.ClientId, bslSettings.ClientSecret)
            };

            var client = new RestClient(options);
            var request = new RestRequest(Constants.BSL_Api_TokenEndpoint, Method.Post);
            request.AddHeader("Authorization", token);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", bslSettings.GrantType);

            var response = await client.ExecuteAsync(request);

            if (!string.IsNullOrEmpty(response.Content))
            {
                return JsonConvert.DeserializeObject<BslTokenResponse>(response.Content);
            }
            return default;
        }

       
        private static string POST(string url, string token, string endpoint, string requestBody)
        {
            // send GET request with RestSharp
            var client = new RestClient(url);
            var request = new RestRequest(endpoint);
            request.AddHeader("Authorization", $"Bearer {token}");

            request.AddHeader("Content-Type", ContentType.Json);
            request.AddBody(requestBody, ContentType.Json);
            var response = client.ExecutePost(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new TokenExpiredException("BSL API token exppired");
            }
            if(!string.IsNullOrEmpty(response.Content))
            {
                return response.Content;
            }
            return string.Empty;
        }

       

        #endregion
    }
}
