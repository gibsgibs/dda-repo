using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ddaproj.Security
{
    public class ReCaptcha
    {
        private static async Task<HttpResponseMessage> GetResponse(string gRecaptchaResponse)
        {
            HttpClient httpClient = new HttpClient();
            var secret = Startup.StaticConfiguration["RecaptchaSettings:SecretKey"];
            var uri = $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={gRecaptchaResponse}";
            return await httpClient.GetAsync(uri);
        }
        public static async Task<bool> IsValid(string gRecaptchaResponse)
        {
            var response = await GetResponse(gRecaptchaResponse);
            var JsonResponse = await response.Content.ReadAsStringAsync();
            var success = $"{JObject.Parse(JsonResponse)["success"]}";
            return success.Equals("True");
        }
    }
}
