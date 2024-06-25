// Copyright Schneider-Electric 2024

﻿using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ETPX.FirmwareRepository.BSL.Entities
{
    /// <summary>
    /// BSL API token response
    /// </summary>
    public class BslTokenResponse
    {
        /// <summary>
        /// Refresh Token Expires in 
        /// </summary>
        [JsonProperty("refresh_token_expires_in")]
        public string RefreshTokenExpiresIn { get; set; }

        /// <summary>
        /// Api Product List
        /// </summary>
        [JsonProperty("api_product_list")]
        public string ApiProductList { get; set; }

        /// <summary>
        /// Api Product List Json
        /// </summary>
        [JsonProperty("api_product_list_json")]
        public List<string> ApiProductListJson { get; set; }

        /// <summary>
        /// Organization Name
        /// </summary>
        [JsonProperty("organization_name")]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Developer Email
        /// </summary>
        [JsonProperty("developeremail")]
        public string DeveloperEmail { get; set; }

        /// <summary>
        /// Token Type
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }


        /// <summary>
        /// Issued At
        /// </summary>
        [JsonProperty("issued_at")]
        public string IssuedAt { get; set; }

        /// <summary>
        /// Client Id
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Access Token
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Application Name
        /// </summary>
        [JsonProperty("application_name")]
        public string ApplicationName { get; set; }

        /// <summary>
        /// Scope
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }


        /// <summary>
        /// ExpiresIn
        /// </summary>
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }


        /// <summary>
        /// Refresh Count
        /// </summary>
        [JsonProperty("refresh_count")]
        public string RefreshCount { get; set; }


        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BslTokenResponse()
        {
            this.RefreshTokenExpiresIn = string.Empty;
            this.RefreshCount = string.Empty;
            this.Status = string.Empty;
            this.ApiProductList = string.Empty;
            this.IssuedAt = string.Empty;
            this.DeveloperEmail = string.Empty;
            this.Scope = string.Empty;
            this.AccessToken = string.Empty;
            this.ApplicationName = string.Empty;
            this.ClientId = string.Empty;
            this.ExpiresIn = string.Empty;
            this.OrganizationName = string.Empty;
            this.TokenType = string.Empty;
            this.ApiProductListJson = new List<string> { };
        }
    }
}
