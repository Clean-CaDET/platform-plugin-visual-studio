using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Clean_CaDET.Model.PlatformConnection.DTOs;
using Newtonsoft.Json;

namespace Clean_CaDET.Model.PlatformConnection
{
    public sealed class CaDETConnection : IPlatformConnection
    {
        private readonly HttpClient _httpClient = new HttpClient();
        //TODO:Refactor to be read from configuration
        private readonly string codeUrlRepositoryCompiler = "https://localhost:44325/api/repository/education/class";
        private readonly string codeUrlSmartTutor = "https://localhost:44333/api/smarttutor/education/class";

        //TODO: Delete this two Lists and Logic about them in this method, only testing purposes
        List<Guid> sentRequests = new List<Guid>();
        List<Guid> recivedRequests = new List<Guid>();

        public async Task<ClassQualityAnalysisResponse> GetClassQualityAnalysisAsync(string sourceCode)
        {
            StringContent requestRepositoryCompiler = new StringContent(JsonConvert.SerializeObject(sourceCode), Encoding.UTF8, "application/json");
            HttpResponseMessage responseRepositoryCompiler = await _httpClient.PostAsync(codeUrlRepositoryCompiler, requestRepositoryCompiler);
            string contentRepositoryCompiler = await responseRepositoryCompiler.Content.ReadAsStringAsync();

            ClassQualityAnalysisResponse repositoryCompilerResponse = JsonConvert.DeserializeObject<ClassQualityAnalysisResponse>(contentRepositoryCompiler);

            

            ClassQualityAnalysisResponse analysis = await SendRequestToSmartTutor(repositoryCompilerResponse);

            var EmptyGuid = new Guid(new Byte[16]);
            Guid sentRequest = repositoryCompilerResponse.Id;
            Guid recivedRequest = analysis.Id;

            sentRequests.Add(sentRequest);
            recivedRequests.Add(recivedRequest);

            while (recivedRequest.Equals(EmptyGuid) || !(sentRequest.Equals(recivedRequest)))
            { 
                analysis = await SendRequestToSmartTutor(repositoryCompilerResponse);
            }


            if (sentRequests.All(recivedRequests.Contains) && sentRequests.Count == recivedRequests.Count)
            {
                System.Diagnostics.Debug.WriteLine("All sent messages have arrived");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Not all sent messages have arrived");
            }

            return analysis;
        }

        private async Task<ClassQualityAnalysisResponse> SendRequestToSmartTutor(ClassQualityAnalysisResponse repositoryCompilerResponse)
        {
            StringContent requestSmartTutor = new StringContent(JsonConvert.SerializeObject(repositoryCompilerResponse.Id), Encoding.UTF8, "application/json");
            HttpResponseMessage responseSmartTutor = await _httpClient.PostAsync(codeUrlSmartTutor, requestSmartTutor);
            string contentSmartTutor = await responseSmartTutor.Content.ReadAsStringAsync();

            var analysis = JsonConvert.DeserializeObject<ClassQualityAnalysisResponse>(contentSmartTutor);
            return analysis;
        }
    }
}