using Clean_CaDET.Model.CodeCompiler;
using Clean_CaDET.Model.CodeCompiler.Data;
using Clean_CaDET.Model.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clean_CaDET.Model
{
    public sealed class PlatformService
    {
        #region Singleton
        private static readonly PlatformService _instance = new PlatformService();
        static PlatformService() {}
        private PlatformService() { }

        public static PlatformService Instance { get
            {
                return _instance;
            }
        }
        #endregion

        private readonly HttpClient httpClient = new HttpClient();
        private readonly string eduUrl = "https://localhost:44348/api/content";
        private readonly string codeUrl = "https://localhost:44348/api/analysis";

        public async Task<List<EduSnippet>> GetEducationalContentAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync(eduUrl);
            string responseContent = await response.Content.ReadAsStringAsync();
            List<EduSnippet> eduVideos = JsonConvert.DeserializeObject<List<EduSnippet>>(responseContent);
            return eduVideos;
        }

        public async Task<string> SendCodeAsync()
        {
            SolutionCompiler st = new SolutionCompiler();
            CaDETSolution solution = await st.CompileBaseSolutionAsync();

            StringContent request = CreateCodeAnalysisRequest(solution);
            HttpResponseMessage response = await httpClient.PostAsync(codeUrl, request);
            return response.StatusCode.ToString();
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
