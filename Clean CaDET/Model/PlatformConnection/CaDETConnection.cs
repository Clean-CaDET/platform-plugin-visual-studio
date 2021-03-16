using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Clean_CaDET.Model.PlatformConnection.DTOs;
using Newtonsoft.Json;

namespace Clean_CaDET.Model.PlatformConnection
{
    public sealed class CaDETConnection: IPlatformConnection
    {
        private readonly HttpClient _httpClient = new HttpClient();
        //TODO:Refactor to be read from configuration
        private readonly string codeUrl = "https://localhost:44325/api/repository/education/class";

        public async Task<ClassQualityAnalysisResponse> GetClassQualityAnalysisAsync(string sourceCode)
        {
            StringContent request = new StringContent(JsonConvert.SerializeObject(sourceCode), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(codeUrl, request);
            string content = await response.Content.ReadAsStringAsync();
            //TODO: Delete this print 
            System.Diagnostics.Debug.WriteLine(content);
            return JsonConvert.DeserializeObject<ClassQualityAnalysisResponse>(content);
        }
    }
}