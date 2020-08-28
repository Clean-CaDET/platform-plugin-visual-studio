using Clean_CaDET.Model.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
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
        private readonly string url = "https://localhost:44348/api/content";

        public async Task<List<EduSnippet>> GetEducationalContentAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);
            string responseContent = await response.Content.ReadAsStringAsync();
            List<EduSnippet> eduVideos = JsonConvert.DeserializeObject<List<EduSnippet>>(responseContent);
            return eduVideos;
        }
    }
}
