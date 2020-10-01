using Clean_CaDET.Model.CodeCompiler;
using Clean_CaDET.Model.CodeCompiler.Data;
using Clean_CaDET.Model.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Clean_CaDET.Model.DTOs;

namespace Clean_CaDET.Model
{
    public sealed class PlatformService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly SolutionExplorer _explorer = new SolutionExplorer();
        private readonly string eduUrl = "https://localhost:44348/api/content";
        private readonly string codeUrl = "https://localhost:44325/api/repository/parse/class";

        public async Task<List<EduSnippet>> GetEducationalContentAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(eduUrl);
            string responseContent = await response.Content.ReadAsStringAsync();
            List<EduSnippet> eduVideos = JsonConvert.DeserializeObject<List<EduSnippet>>(responseContent);
            return eduVideos;
        }

        public async Task<string> SendAllCodeAsync()
        {
            CaDETSolution solution = await _explorer.CompileBaseSolutionAsync();

            StringContent request = CreateCodeAnalysisRequest(solution);
            HttpResponseMessage response = await _httpClient.PostAsync(codeUrl, request);
            return response.StatusCode.ToString();
        }

        public async Task<CaDETClassDTO> SendClassCodeAsync(string classPath)
        {
            string sourceCode = await _explorer.FindClassCode(classPath);

            StringContent request = new StringContent(JsonConvert.SerializeObject(sourceCode), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(codeUrl, request);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CaDETClassDTO>(content);
        }

        private StringContent CreateCodeAnalysisRequest(CaDETSolution solution)
        {
            //The first convert serializes an object to JSON, while the second serializes a string to JSON
            //https://stackoverflow.com/questions/59088650/post-string-to-rest-api-results-in-400-bad-request
            //The backend expects a string, but we might refactor this
            string objectJson = JsonConvert.SerializeObject(solution);
            string payload = JsonConvert.SerializeObject(objectJson);
            return new StringContent(payload, Encoding.UTF8, "application/json");
        }
    }
}
