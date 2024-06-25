// Copyright Schneider-Electric 2024

﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ETPX.FirmwareRepository.Domain.Entities
{

    /// <summary>
    /// Audience
    /// </summary>
    public class Audience
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Audience()
        {
            this.Name = string.Empty;
        }
    }

    /// <summary>
    /// Document Count
    /// </summary>
    public class Count
    {
        /// <summary>
        /// Number Of Docs
        /// </summary>
        [JsonProperty("numberOfDocs")]
        public int NumberOfDocs { get; set; }

        /// <summary>
        /// Old Document Id
        /// </summary>
        [JsonProperty("oid")]
        public int? Oid { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public Name Name { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Count()
        {
            this.NumberOfDocs = 0;
            this.Oid = 0;
            this.Name = new Name();
        }

    }

    /// <summary>
    /// Document Type Group
    /// </summary>
    public class DocTypeGroup
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Translation
        /// </summary>
        [JsonProperty("translation")]
        public string Translation { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DocTypeGroup()
        {
            this.Id = 0;
            this.Translation = string.Empty;
        }
    }

    /// <summary>
    /// Document Type Groups
    /// </summary>
    public class DocTypeGroups
    {
        /// <summary>
        /// Document Type Group
        /// </summary>
        [JsonProperty("docTypeGroup")]
        public List<DocTypeGroup> DocTypeGroup { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DocTypeGroups()
        {
            this.DocTypeGroup = new List<DocTypeGroup>();
        }
    }

    /// <summary>
    /// Document
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Author
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; set; }

        /// <summary>
        /// Doc Oid
        /// </summary>
        [JsonProperty("docOid")]
        public object DocOid { get; set; }


        /// <summary>
        /// Reference
        /// </summary>
        [JsonProperty("reference")]
        public string Reference { get; set; }

        /// <summary>
        /// Audience
        /// </summary>
        [JsonProperty("audience")]
        public Audience Audience { get; set; }


        /// <summary>
        /// CreationDate
        /// </summary>
        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }


        /// <summary>
        /// DocOwner
        /// </summary>
        [JsonProperty("docOwner")]
        public string DocOwner { get; set; }

        /// <summary>
        /// DocTypeGroups
        /// </summary>
        [JsonProperty("docTypeGroups")]
        public DocTypeGroups DocTypeGroups { get; set; }

        /// <summary>
        /// DocumentDate
        /// </summary>
        [JsonProperty("documentDate")]
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// DocumentType
        /// </summary>
        [JsonProperty("documentType")]
        public DocumentType DocumentType { get; set; }

        /// <summary>
        /// Files
        /// </summary>
        [JsonProperty("files")]
        public Files Files { get; set; }

        /// <summary>
        /// FlipFlopGenerated
        /// </summary>
        [JsonProperty("flipFlopGenerated")]
        public bool FlipFlopGenerated { get; set; }

        /// <summary>
        /// LastModificationDate
        /// </summary>
        [JsonProperty("lastModificationDate")]
        public DateTime LastModificationDate { get; set; }

        /// <summary>
        /// Locales
        /// </summary>
        [JsonProperty("locales")]
        public Locales Locales { get; set; }


        /// <summary>
        /// NumberOfPage
        /// </summary>
        [JsonProperty("numberOfPage")]
        public int NumberOfPage { get; set; }

        /// <summary>
        /// ProductReferences
        /// </summary>
        [JsonProperty("productReferences")]
        public ProductReferences ProductReferences { get; set; }

        /// <summary>
        /// Revision
        /// </summary>
        [JsonProperty("revision")]
        public string Revision { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// PartNumber
        /// </summary>
        [JsonProperty("partNumber")]
        public string PartNumber { get; set; }

        /// <summary>
        /// PublicationDate
        /// </summary>
        [JsonProperty("publicationDate")]
        public DateTime? PublicationDate { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Document()
        {
            this.NumberOfPage = 0;
            this.PartNumber = string.Empty;
            this.Version = string.Empty;
            this.ProductReferences = new ProductReferences();
            this.Revision = string.Empty;
            this.Reference = string.Empty;
            this.Description = string.Empty;
            this.Audience = new Audience();
            this.Author = string.Empty;
            this.Description = string.Empty;
            this.CreationDate = DateTime.MinValue;
            this.DocumentDate = DateTime.MinValue;
            this.DocOid = string.Empty;
            this.DocOwner = string.Empty;
            this.DocTypeGroups = new DocTypeGroups();
            this.Locales = new Locales();
            this.DocumentType = new DocumentType();
            this.Files = new Files();
            this.Title = string.Empty;
        }
    }

    /// <summary>
    /// Document count
    /// </summary>
    public class DocumentCount
    {
        /// <summary>
        /// Field
        /// </summary>
        [JsonProperty("field")]
        public string Field { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        [JsonProperty("count")]
        public List<Count> Count { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentCount()
        {
            this.Count = new List<Count>();
            this.Field = string.Empty;
        }
    }
    
    /// <summary>
    /// Document Counts
    /// </summary>
    public class DocumentCounts
    {
        /// <summary>
        /// Document count object
        /// </summary>
        [JsonProperty("documentCount")]
        public List<DocumentCount> DocumentCount { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentCounts()
        {
             this.DocumentCount = new List<DocumentCount>();
        }
    }

    /// <summary>
    /// List of documents
    /// </summary>
    public class Documents
    {

        /// <summary>
        /// Document
        /// </summary>
        [JsonProperty("document")]
        public List<Document> Document { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Documents()
        {
            this.Document = new List<Document>();
        }
    }

    /// <summary>
    /// Document Type
    /// </summary>
    public class DocumentType
    {
        /// <summary>
        /// English label of document
        /// </summary>
        [JsonProperty("englishLabel")]
        public string EnglishLabel { get; set; }

        /// <summary>
        /// Document Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Document Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Translated name of document
        /// </summary>
        [JsonProperty("translation")]
        public string Translation { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentType()
        {
                this.EnglishLabel = string.Empty;
            this.Id = 0;
            this.Name = string.Empty;
            this.Translation = string.Empty;
        }
    }

    /// <summary>
    /// Document File Details
    /// </summary>
    public class File
    {
        /// <summary>
        /// File Extension
        /// </summary>
        [JsonProperty("extension")]
        public string Extension { get; set; }

        /// <summary>
        /// FileName
        /// </summary>
        [JsonProperty("filename")]
        public string Filename { get; set; }

        /// <summary>
        /// File Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// SHA256 Hash code of file
        /// </summary>
        [JsonProperty("sha256")]
        public string Sha256 { get; set; }

        /// <summary>
        /// File size in bytes
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; set; }

        /// <summary>
        /// Download link of file
        /// </summary>
        [JsonProperty("downloadLink")]
        public string DownloadLink { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public File()
        {
            this.Filename = string.Empty;
            this.Size = 0;
            this.Extension = string.Empty;
            this.Filename = string.Empty;
            this.Sha256 = string.Empty;
            this.DownloadLink = string.Empty;
            this.Id = string.Empty;
        }
    }

    /// <summary>
    /// List of document files
    /// </summary>
    public class Files
    {
        /// <summary>
        /// Document File.
        /// </summary>
        [JsonProperty("file")]
        public List<File> File { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Files()
        {
            this.File = new List<File>();
        }
    }

    /// <summary>
    /// GetDocumentPageResponse
    /// </summary>
    public class GetDocumentPageResponse
    {
        /// <summary>
        /// Return object 
        /// </summary>
        [JsonProperty("return")]
        public Return Return { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GetDocumentPageResponse()
        {
            this.Return = new Return();
        }
    }

    /// <summary>
    /// Local
    /// </summary>
    public class Locale
    {
        /// <summary>
        /// ISO country Name
        /// </summary>
        [JsonProperty("isoCountry")]
        public string IsoCountry { get; set; }


        /// <summary>
        /// ISO Language
        /// </summary>
        [JsonProperty("isoLanguage")]
        public string IsoLanguage { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Locale()
        {
            this.IsoCountry = string.Empty;
            this.IsoLanguage = string.Empty;
        }
    }

    /// <summary>
    /// list of locals
    /// </summary>
    public class Locales
    {
        /// <summary>
        /// Local
        /// </summary>
        [JsonProperty("locale")]
        public List<Locale> Locale { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Locales()
        {
            this.Locale = new List<Locale>();
        }
    }

    /// <summary>
    /// Name
    /// </summary>
    public class Name
    {
        /// <summary>
        /// Locale
        /// </summary>
        [JsonProperty("locale")]
        public string Locale { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Highlighted
        /// </summary>
        [JsonProperty("highlighted")]
        public string Highlighted { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Name()
        {
            this.Value = string.Empty;
            this.Locale = string.Empty;
            this.Highlighted = string.Empty;
        }
    }

    /// <summary>
    /// Product reference
    /// </summary>
    public class ProductReference
    {
        /// <summary>
        /// Commercial reference
        /// </summary>
        [JsonProperty("commRef")]
        public string CommRef { get; set; }

        /// <summary>
        /// Is Main Product reference
        /// </summary>
        [JsonProperty("main")]
        public bool Main { get; set; }

        /// <summary>
        /// Is Alternate Product reference
        /// </summary>
        [JsonProperty("alternative")]
        public bool Alternative { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductReference()
        {
            this.Main = false;
            this.Alternative = false;
            this.CommRef = string.Empty;
        }
    }

    /// <summary>
    /// List of product reference
    /// </summary>
    public class ProductReferences
    {
        /// <summary>
        /// List of product reference
        /// </summary>
        [JsonProperty("productReference")]
        public List<ProductReference> ProductReference { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductReferences()
        {
            this.ProductReference = new List<ProductReference>();
        }
    }


    /// <summary>
    /// Document page search response return
    /// </summary>
    public class Return
    {
        /// <summary>
        /// Document counts
        /// </summary>
        [JsonProperty("documentCounts")]
        public DocumentCounts DocumentCounts { get; set; }

        /// <summary>
        /// List of documents
        /// </summary>
        [JsonProperty("documents")]
        public Documents Documents { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Return()
        {
            this.Documents = new Documents();
            this.DocumentCounts = new DocumentCounts();
        }
    }

    /// <summary>
    /// Document page search response
    /// </summary>
    public class DocRefPageResponse
    {
        /// <summary>
        /// Document page search response
        /// </summary>
        [JsonProperty("getDocumentPageResponse")]
        public GetDocumentPageResponse GetDocumentPageResponse { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DocRefPageResponse()
        {
            this.GetDocumentPageResponse = new GetDocumentPageResponse();
        }
    }
}
