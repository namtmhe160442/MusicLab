using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using MusicLab.Backend.Services.Interfaces;

namespace MusicLab.Backend.Services
{
    public class AWSS3Service : IAWSS3Service
    {
        private readonly IConfiguration _configuration;
        public AWSS3Service(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GetURLAsync(string songName)
        {
            try
            {
                var credentials = new BasicAWSCredentials(_configuration["AWSS3:AccessKey"], _configuration["AWSS3:SecretKey"]);
                var config = new AmazonS3Config();
                config.ServiceURL = _configuration["AWSS3:ServiceURL"];
                using var client = new AmazonS3Client(credentials, config);
                var request = new GetPreSignedUrlRequest
                {
                    BucketName = _configuration["AWSS3:BucketName"],
                    Key = songName,
                    Expires = DateTime.Now.AddYears(1)
                };
                var response = client.GetPreSignedURL(request);
                return response;
            }
            catch (Exception ex)
            {
                return null;
            }

            
        } 
    }
}
