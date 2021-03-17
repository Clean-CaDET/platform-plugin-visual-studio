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
        private readonly string codeUrlRepositoryCompiler = "https://localhost:44325/api/repository/education/class";
        private readonly string codeUrlSmartTutor = "https://localhost:44333/api/smarttutor/education/class";

        public async Task<ClassQualityAnalysisResponse> GetClassQualityAnalysisAsync(string sourceCode)
        {
            //StringContent requestRepositoryCompiler = new StringContent(JsonConvert.SerializeObject(sourceCode), Encoding.UTF8, "application/json");
            //HttpResponseMessage responseRepositoryCompiler = await _httpClient.PostAsync(codeUrlRepositoryCompiler, requestRepositoryCompiler);
            //string contentRepositoryCompiler = await responseRepositoryCompiler.Content.ReadAsStringAsync();

            StringContent requestSmartTutor = new StringContent(JsonConvert.SerializeObject(sourceCode), Encoding.UTF8, "application/json");
            HttpResponseMessage responseSmartTutor = await _httpClient.PostAsync(codeUrlSmartTutor, requestSmartTutor);
            string contentSmartTutor = await responseSmartTutor.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ClassQualityAnalysisResponse>(contentSmartTutor);
        }


    }
}