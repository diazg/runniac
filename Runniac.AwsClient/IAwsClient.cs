using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Runniac.Aws
{
    public interface IAwsClient
    {
        void UploadFile(string bucketName, string fileName, Stream file, string mime);
    }
}
