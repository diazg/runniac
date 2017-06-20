using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Runniac.Utils
{
    /// <summary>
    /// Provides various image untilities, such as high quality resizing and the ability to save a JPEG.
    /// </summary>
    public static class GisUtils
    {
        private static Dictionary<string, string> _acceptedMimeTypes = new Dictionary<string, string> 
            {
                { "application/vnd.google-earth.kml+xml", "kml" }, 
                { "application/octet-stream", "kml" },                 
                { "application/gpx+xml", "gpx" }
            };
        public const int MAX_IMAGE_SIZE = 5242880;// 5MB

        /// <summary>
        /// Comprueba si el tipo MIME recibido corresponde a una imagen.
        /// </summary>
        /// <param name="extension">Tipo MIME del archivo.</param>
        /// <returns>True si el tipo MIME es de una imagen.</returns>
        public static bool IsGisFile(string extension)
        {
            if (String.IsNullOrEmpty(extension))
                return false;

            return _acceptedMimeTypes.ContainsValue(extension.TrimStart('.'));
        }

        /// <summary>
        /// Retorna la extensión de fichero asociada a un tipo mime.
        /// </summary>
        /// <param name="mimeType">Tipo MIME.</param>
        /// <returns>Extensión asociada.</returns>
        public static string GetExtensionFromMime(string mimeType)
        {
            return _acceptedMimeTypes[mimeType];
        }
    }
}
