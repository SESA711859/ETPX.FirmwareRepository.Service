// Copyright Schneider-Electric 2024

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ETPX.FirmwareRepository.BSL.Entities
{
    /// <summary>
    /// Doc Pages Search Request
    /// </summary>
    public class DocPagesSearchRequest
    {

        /// <summary>
        /// GetDocumentPage
        /// </summary>
        [JsonPropertyName("getDocumentPage")]
        public GetDocumentPage GetDocumentPage { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DocPagesSearchRequest()
        {
            this.GetDocumentPage = new GetDocumentPage();
        }
    }

    /// <summary>
    /// Get Document Page
    /// </summary>
    public class GetDocumentPage
    {
        /// <summary>
        /// Scope
        /// </summary>
        [JsonPropertyName("scope")]
        public Scope Scope { get; set; }


        /// <summary>
        /// Query
        /// </summary>
        [JsonPropertyName("query")]
        public Query Query { get; set; }


        /// <summary>
        /// Pagination
        /// </summary>
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; }


        /// <summary>
        /// Version
        /// </summary>
        [JsonPropertyName("version")]
        public int Version { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GetDocumentPage()
        {
            this.Scope = new Scope();
            this.Query = new Query();
            this.Version = 0;
            this.Pagination = new Pagination();
        }
    }

    /// <summary>
    /// Scope
    /// </summary>
    public class Scope
    {
        /// <summary>
        /// Brand
        /// </summary>
        [JsonPropertyName("brand")]
        public string Brand { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Scope()
        {
            Brand = string.Empty;
            Country = string.Empty;
        }
    }

    /// <summary>
    /// Pagination
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// FirstResult
        /// </summary>
        [JsonPropertyName("firstResult")]
        public int FirstResult { get; set; }

        /// <summary>
        /// MaxResult
        /// </summary>
        [JsonPropertyName("maxResult")]
        public int MaxResult { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Pagination()
        {
            this.FirstResult = 1;
            this.MaxResult = 2;
        }
    }

    /// <summary>
    /// Query
    /// </summary>
    public class Query
    {
        /// <summary>
        /// ProductCommRef
        /// </summary>
        [JsonPropertyName("productCommRef")]
        public List<string> ProductCommRef { get; set; }

        /// <summary>
        /// DocTypes
        /// </summary>
        [JsonPropertyName("docTypes")]
        public List<string> DocTypes { get; set; }

        /// <summary>
        /// FileExtensions
        /// </summary>
        [JsonPropertyName("fileExtensions")]
        public List<string> FileExtensions { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Query()
        {
            this.ProductCommRef = new List<string>();
            this.FileExtensions = new List<string>();
            this.DocTypes = new List<string>();
        }
    }
}
