using Clean_CaDET.Model.CodeCompiler;
using Clean_CaDET.Model.DTOs;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clean_CaDET.Model
{
    public sealed class PlatformService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly SolutionExplorer _explorer = new SolutionExplorer();
        private readonly string codeUrl = "https://localhost:44325/api/repository/education/class";

        public async Task<ClassQualityAnalysisResponse> AnalyzeClassQualityAsync(string classPath)
        {
            string sourceCode = await _explorer.FindClassCodeAsync(classPath);

            StringContent request = new StringContent(JsonConvert.SerializeObject(sourceCode), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(codeUrl, request);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ClassQualityAnalysisResponse>(content);
        }
    }
}
