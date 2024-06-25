// Copyright Schneider-Electric 2024

using System.Collections.Immutable;

namespace ETPX.FirmwareRepository
{
    /// <summary>
    /// List of Constants 
    /// </summary>
    public static class Constants
    {
        /// <summary> String regex </summary>
        public static readonly string Regex_String = @"^([a-zA-Z ]*)$";

        /// <summary> Regex generic Error Message </summary>
        public static readonly string Regex_GenericInvalidErrorMessage = " 'PropertyName' is invalid input parameter";

        /// <summary> Regex generic Error Message </summary>
        public static readonly string Regex_InvalidErrorMessage = "{0} is invalid input parameter";

        /// <summary> Regex generic Error Message </summary>
        public static readonly string Regex_InvalidEnumErrorMessage = "'PropertyName' must be valid enum value.";

        /// <summary> Guid regex </summary>
        public static readonly string Regex_Guid = @"^([a-zA-Z0-9-]*)$";

        /// <summary> Decimal regex </summary>
        public static readonly string Regex_Decimal = @"^([0-9.]*)$";

        /// <summary> Device value regex </summary>
        public static readonly string Regex_DeviceValue = @"^([a-zA-Z0-9. ]*)$";

        /// <summary> EmailId regex </summary>
        public static readonly string Regex_EmailId = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        /// <summary> Regex_Alphanumeric regex </summary>
        public static readonly string Regex_Alphanumeric = @"^([a-zA-Z0-9 ]*)$";

        /// <summary>
        /// BSL Token Service API endpoint
        /// </summary>
        public static readonly string BSL_Api_TokenEndpoint = "token";

        /// <summary>
        /// BSL document page search endpoint.
        /// </summary>
        public static readonly string BSL_DocRef_pages_search_endpoint = "document-pages/search";

        /// <summary>
        /// Firmware download link end point
        /// </summary>
        public static readonly string FW_Download_link_EndPoint = "files?p_enDocType=Firmware&p_File_Name={0}&p_Doc_Ref={1}";

        /// <summary>
        /// Latest firmware version not Found
        /// </summary>
        public static readonly string Latest_FW_Not_Found_Message = "Latest firmware version not found";

        /// <summary>
        /// Specific firmware version not Found
        /// </summary>
        public static readonly string Specific_FW_Not_Found_Message = "Specific firmware version not found";

        /// <summary>
        /// firmware version not Found
        /// </summary>
        public static readonly string FW_Not_Found_Message = "Firmware version not found";

        /// <summary>
        /// firmware files not Found
        /// </summary>
        public static readonly string FW_Files_Not_Found_Message = "Firmware files not found";

        /// <summary> Commercial Reference regex </summary>
        public static readonly string Regex_CommercialReference = @"^([a-zA-Z0-9_ -]*)$";

        /// <summary>
        /// Supported Firmware file extensions
        /// </summary>
        public readonly static ImmutableList<string> SupportedFirmwareExtension = ImmutableList.Create(".sedp", ".s19", ".sch", ".sp1", ".fw", ".bin");

        /// <summary> ComponentName </summary>
        public const string ComponentName = "componentName";

        /// <summary> ComponentValue </summary>
        public const string ComponentValue = "commissioning service";
    }
}