using Clean_CaDET.Model.PlatformConnection.DTOs.QualityAnalysis;
using Clean_CaDET.Model.PlatformConnection.DTOs.SubmissionEvaluation;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clean_CaDET.Model.PlatformConnection
{
    public sealed class CaDETConnection: IPlatformConnection
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl;

        public CaDETConnection(string url)
        {
            _baseUrl = url;
        }

        public async Task<ChallengeEvaluationDTO> SubmitChallengeAsync(string[] sourceCode, int challengeId, int learnerId)
        {
            var payload = new ChallengeSubmissionDTO(sourceCode, challengeId, learnerId);
            var request = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync(_baseUrl + "submissions/challenge", request);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ChallengeEvaluationDTO>(content);
        }

        public async Task<CodeEvaluationDTO> EvaluateCodeQualityAsync(string[] sourceCode)
        {
            var payload = new CodeSubmissionDTO {SourceCode = sourceCode, LearnerId = 1};
            var request = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl + "code-analysis/", request);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CodeEvaluationDTO>(content);
        }
    }
}