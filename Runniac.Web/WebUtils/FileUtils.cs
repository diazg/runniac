using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Runniac.Web.WebUtils
{
    public static class FileUtils
    {
        /// <summary>
        /// Obtiene todos los ficheros subidos al directorio temporal.
        /// </summary>
        /// <returns>Una referencia al proveedor de datos que utiliza este directorio como fuente.</returns>
        public static MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var uploadFolder = ConfigurationManager.AppSettings["TempFilesFolder"].ToString();
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        public static string GetFileExtension(string fileType)
        {
            if (fileType.Contains("kml"))
                return "kml";
            else if (fileType.Contains("gpx"))
                return "gpx";
            else
                return string.Empty;
        }

        /// <summary>
        /// Guarda un fichero en el disco.
        /// </summary>
        /// <param name="file">Fichero a guardar.</param>
        /// <param name="name">Nombre con el que se guardará el fichero.</param>
        /// <param name="path">Ruta en la que se guardará el fichero.</param>
        public static void SaveFileToDisk(MultipartFileData file, string name, string path)
        {
            using (var fileStream = File.Create(
                Path.Combine(HttpContext.Current.Server.MapPath(path), name)))
            {
                var fileContent = new StreamReader(file.LocalFileName).BaseStream;
                fileContent.Seek(0, SeekOrigin.Begin);
                fileContent.CopyTo(fileStream);
            }
        }
    }
}