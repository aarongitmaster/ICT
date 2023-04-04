using System.Net;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;

namespace ICTTaxApi.Tools
{
    public static class FileTaxUploader
    {

        public static async Task Upload(IConfiguration config, IFormFile file, string filename)
        {
            var uri = config.GetValue<string>("AzureFunctionBaseURL");
            var uploadFunctionURL = config.GetValue<string>("AFUploadFileURL");

            var client = new HttpClient
            {
                //BaseAddress = String.Format(@"{0}{1}", uri, uploadFunctionURL);
            };

            var requestUri = string.Format("{0}{1}", uri, uploadFunctionURL);
            await using var stream = file.OpenReadStream();
            using var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            using var content = new MultipartFormDataContent
            {
                { new StreamContent(stream), "file", filename}
            };

            request.Content = content;

            try
            {
                await client.SendAsync(request);
            }
            catch (Exception ex)
            {

            }
        }

    }
}
