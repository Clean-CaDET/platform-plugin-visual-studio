using System;
using System.Collections.Generic;
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

            var bytes = new Byte[16];
            var EmptyGuid = new Guid(bytes);

            ClassQualityAnalysisResponse analysis = await SendRequestToSmartTutor(repositoryCompilerResponse);

            sentRequests.Add(repositoryCompilerResponse.Id);
            recivedRequests.Add(analysis.Id);

            while (analysis.Id.Equals(EmptyGuid) || !(repositoryCompilerResponse.Id.Equals(analysis.Id)))
            { // TODO: Check edge case when we do not have a issue and content for developer
                analysis = await SendRequestToSmartTutor(repositoryCompilerResponse);
            }

            System.Diagnostics.Debug.WriteLine(sentRequests);

            System.Diagnostics.Debug.WriteLine("POSLATEEEEEEEEEE");
            int counter = 0;
            foreach (Guid guid in sentRequests){
                counter++;
                System.Diagnostics.Debug.WriteLine(counter);
                System.Diagnostics.Debug.WriteLine(guid);
            }
            System.Diagnostics.Debug.WriteLine("PRIMLJENEEEEEEE");
            counter = 0;
            foreach (Guid guid in recivedRequests)
            {
                counter++;
                System.Diagnostics.Debug.WriteLine(counter);
                System.Diagnostics.Debug.WriteLine(guid);
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