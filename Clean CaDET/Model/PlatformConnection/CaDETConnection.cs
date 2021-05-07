using Clean_CaDET.Model.PlatformConnection.DTOs;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clean_CaDET.Model.PlatformConnection
{
    public sealed class CaDETConnection: IPlatformConnection
    {
        private readonly HttpClient _httpClient = new HttpClient();
        //TODO:Refactor to be read from configuration
        private readonly string baseUrl = "https://localhost:44325/api/";

        public async Task<ChallengeEvaluationDTO> SubmitChallengeAsync(string[] sourceCode, int challengeId, int learnerId)
        {
            var payload = new ChallengeSubmissionDTO(sourceCode, challengeId, learnerId);
            StringContent request = new StringContent(JsonConvert.SerializeObject(sourceCode), Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await _httpClient.PostAsync(baseUrl + "submission/challenge", request);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ChallengeEvaluationDTO>(content);
        }
    }
}