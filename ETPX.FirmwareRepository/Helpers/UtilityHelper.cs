// Copyright Schneider-Electric 2024

﻿namespace ETPX.FirmwareRepository.Helpers
{

    /// <summary>
    /// Utility helper class
    /// </summary>
    public class UtilityHelper
    {
        /// <summary>
        /// Parse firmware version
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public virtual Version ParseVersion(string version)
        {
            try
            {
                version = version.ToUpper().Replace("V", string.Empty).Replace("_", ".");
                var parts = version.Split('.');
                if (parts.Length == 1)
                {
                    return new Version(version + ".0.0");
                }
                else
                {
                    return new Version(version);
                }
            }
            catch(FormatException)
            {
                return new Version("1.0.0.0");
            }
            catch (ArgumentNullException)
            {
                return new Version("1.0.0.0");
            }
            catch (InvalidOperationException)
            {
                return new Version("1.0.0.0");
            }
        }
    }
}
