using Microsoft.AspNetCore.Http;

namespace AutoPartsStore.Helpers
{
    public static class ImageHelper
    {
        public static byte[] ImageToBytes(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            using var ms = new MemoryStream();
            file.CopyTo(ms);
            return ms.ToArray();
        }

        public static string BytesToBase64(byte[] data)
        {
            if (data == null || data.Length == 0)
                return null;

            return $"data:image/jpeg;base64,{Convert.ToBase64String(data)}";
        }
    }
}
