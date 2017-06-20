using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Runniac.Utils
{
    /// <summary>
    /// Provides various image untilities, such as high quality resizing and the ability to save a JPEG.
    /// </summary>
    public static class ImageUtils
    {        
        private static Dictionary<string, string> _acceptedMimeTypes = new Dictionary<string, string> 
            {
                { "image/jpeg", "jpg" }, 
                { "image/gif", "gif" }, 
                { "image/png", "png" }, 
                { "image/bmp", "bmp" } 
            };
        public const int MAX_IMAGE_SIZE = 1048576;

        /// <summary>
        /// Comprueba si el tipo MIME recibido corresponde a una imagen.
        /// </summary>
        /// <param name="mimeType">Tipo MIME del archivo.</param>
        /// <returns>True si el tipo MIME es de una imagen.</returns>
        public static bool IsImage(string mimeType)
        {
            return _acceptedMimeTypes.ContainsKey(mimeType);
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
