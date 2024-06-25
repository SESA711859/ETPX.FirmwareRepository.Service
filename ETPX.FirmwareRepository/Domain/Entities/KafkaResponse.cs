// Copyright Schneider-Electric 2024

using Newtonsoft.Json;

namespace ETPX.FirmwareRepository.Domain.Entities
{
    /// <summary>
    /// Attribute Class
    /// </summary>
    public class Attribute
    {
        /// <summary>
        /// Get or Set Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or Set Value
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Attribute Constructor
        /// </summary>
        public Attribute()
        {
            Name = string.Empty;
            Value = string.Empty;
        }
    }

    /// <summary>
    /// Attributes Class
    /// </summary>
    public class Attributes
    {
        /// <summary>
        /// Get or Set Attribute
        /// </summary>
        [JsonProperty("attribute")]
        public List<Attribute> Attribute { get; set; }

        /// <summary>
        /// Attributes Constructor
        /// </summary>
        public Attributes()
        {
            Attribute = new List<Attribute>();
        }
    }

    /// <summary>
    /// Channel Class
    /// </summary>
    public class Channel
    {
        /// <summary>
        /// Get or Set Key
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// Get or Set Oid
        /// </summary>
        [JsonProperty("oid")]
        public long Oid { get; set; }

        /// <summary>
        /// Get or Set Translations
        /// </summary>
        [JsonProperty("translations")]
        public Translations Translations { get; set; }

        /// <summary>
        /// Channel Constructor
        /// </summary>
        public Channel()
        {
            this.Key = string.Empty;
            this.Translations = new Translations();
        }
    }

    /// <summary>
    /// Channels Class
    /// </summary>
    public class Channels
    {
        /// <summary>
        /// Get or Set Channel
        /// </summary>
        [JsonProperty("channel")]
        public List<Channel> Channel { get; set; }

        /// <summary>
        /// Channels Constructor
        /// </summary>
        public Channels()
        {
            Channel = new List<Channel>();
        }
    }


    /// <summary>
    /// Range Class
    /// </summary>
    public class Range
    {
        /// <summary>
        /// Get or Set Id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        ///Get or Set RangeId
        /// </summary>
        [JsonProperty("rangeId")]
        public string RangeId { get; set; }

        /// <summary>
        /// Get or Set Translation
        /// </summary>
        [JsonProperty("translation")]
        public string Translation { get; set; }

        /// <summary>
        /// Get or Set Status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Range Constructor
        /// </summary>
        public Range()
        {
            this.Status = string.Empty;
            this.RangeId = string.Empty;
            this.Translation = string.Empty;
        }
    }

    /// <summary>
    /// Ranges Class
    /// </summary>
    public class Ranges
    {
        /// <summary>
        /// Get or Set Range
        /// </summary>
        [JsonProperty("range")]
        public List<Range> Range { get; set; }

        /// <summary>
        /// Ranges Constructor
        /// </summary>
        public Ranges()
        {
            this.Range = new List<Range>();
        }
    }

    /// <summary>
    /// Response Class
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Get or Set Attributes
        /// </summary>
        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        /// <summary>
        /// Get or Set Audience
        /// </summary>
        [JsonProperty("audience")]
        public Audience Audience { get; set; }

        /// <summary>
        /// Get or Set Channels
        /// </summary>
        [JsonProperty("channels")]
        public Channels Channels { get; set; }

        /// <summary>
        /// Get or Set CreationDate
        /// </summary>
        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Get or Set DocId
        /// </summary>
        [JsonProperty("docId")]
        public string DocId { get; set; }

        /// <summary>
        /// Get or Set DocOwner
        /// </summary>
        [JsonProperty("docOwner")]
        public string DocOwner { get; set; }

        /// <summary>
        /// Get or Set DocTypeGroups
        /// </summary>
        [JsonProperty("docTypeGroups")]
        public DocTypeGroups DocTypeGroups { get; set; }

        /// <summary>
        /// Get or Set DocumentDate
        /// </summary>
        [JsonProperty("documentDate")]
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Get or Set DocumentType
        /// </summary>
        [JsonProperty("documentType")]
        public DocumentType DocumentType { get; set; }

        /// <summary>
        /// Get or Set Files
        /// </summary>
        [JsonProperty("files")]
        public Files Files { get; set; }

        /// <summary>
        /// Get or Set LastModificationDate
        /// </summary>
        [JsonProperty("lastModificationDate")]
        public DateTime LastModificationDate { get; set; }

        /// <summary>
        /// Get or Set Locales
        /// </summary>
        [JsonProperty("locales")]
        public Locales Locales { get; set; }

        /// <summary>
        /// Get or Set NumberOfPage
        /// </summary>
        [JsonProperty("numberOfPage")]
        public int NumberOfPage { get; set; }

        /// <summary>
        /// Get or Set Ranges
        /// </summary>
        [JsonProperty("ranges")]
        public Ranges Ranges { get; set; }

        /// <summary>
        /// Get or Set Reference
        /// </summary>
        [JsonProperty("reference")]
        public string Reference { get; set; }

        /// <summary>
        /// Get or Set Title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Get or Set Version
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Get or Set FlipFlopGenerated
        /// </summary>
        [JsonProperty("flipFlopGenerated")]
        public bool FlipFlopGenerated { get; set; }

        /// <summary>
        /// Response Constructor
        /// </summary>
        public Response()
        {
            this.Attributes = new Attributes();
            this.Audience = new Audience();
            this.Channels = new Channels();
            this.DocId = string.Empty;
            this.Version = string.Empty;
            this.Title = string.Empty;
            this.Ranges = new Ranges();
            this.Reference = string.Empty;
            this.Files = new Files();
            this.DocOwner = string.Empty;
            this.DocTypeGroups = new DocTypeGroups();
            this.DocumentType = new DocumentType();
            this.Locales = new Locales();
        }
    }

    /// <summary>
    /// KafkaResponse Class
    /// </summary>
    public class KafkaResponse
    {
        /// <summary>
        /// Get or Set Response
        /// </summary>
        [JsonProperty("response")]
        public Response Response { get; set; }

        /// <summary>
        /// KafkaResponse Construtor
        /// </summary>
        public KafkaResponse()
        {
            this.Response = new Response();
        }
    }

    /// <summary>
    /// Translation Class
    /// </summary>
    public class Translation
    {
        /// <summary>
        /// Get or Set Locale
        /// </summary>
        [JsonProperty("locale")]
        public string Locale { get; set; }

        /// <summary>
        /// Get or Set SeoAlias
        /// </summary>
        [JsonProperty("seoAlias")]
        public string SeoAlias { get; set; }

        /// <summary>
        /// Get or Set Title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Translation Construtor
        /// </summary>
        public Translation()
        {
            Locale = string.Empty;
            SeoAlias = string.Empty;
            Title = string.Empty;
        }
    }

    /// <summary>
    /// Translations Class
    /// </summary>
    public class Translations
    {
        /// <summary>
        /// Get or Set Translation.
        /// </summary>
        [JsonProperty("translation")]
        public List<Translation> Translation { get; set; }

        /// <summary>
        /// Translations Constructor
        /// </summary>
        public Translations()
        {
            Translation = new List<Translation>();
        }
    }
}
