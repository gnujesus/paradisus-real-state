using Microsoft.AspNetCore.Http;

namespace RealStateApp.Core.Application.Helpers
{
    public static class Utils
    {
        public static async Task<IFormFile> ConvertBytesToFileAsync(byte[] fileBytes, string fileName)
        {
            if (fileBytes == null || fileBytes.Length == 0)
            {
                return null;
            }

            var stream = new MemoryStream(fileBytes);
            var formFile = new FormFile(stream, 0, fileBytes.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream"
            };

            return formFile;
        }
    }
}
