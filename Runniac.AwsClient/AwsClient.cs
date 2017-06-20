using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Aws
{
    public class AwsClient : IAwsClient
    {
        private string _accessKey;
        private string _secretKey;
        public const string BASE_URL = "http://s3-eu-west-1.amazonaws.com";

        public AwsClient(string accessKey, string secretKey)
        {
            _accessKey = accessKey;
            _secretKey = secretKey;
        }

        public void UploadFile(string bucketName, string fileName, Stream file, string mime)
        {
            using (var client = new AmazonS3Client(Amazon.RegionEndpoint.EUWest1))
            {
                PutObjectRequest request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName,
                    InputStream = file,
                    CannedACL = S3CannedACL.PublicRead,
                    ContentType = mime
                };
                client.PutObjectAsync(request);                
            }
        }
    }
}
