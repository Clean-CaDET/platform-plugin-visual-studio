using System.Threading.Tasks;
using Clean_CaDET.Model.PlatformConnection.DTOs;

namespace Clean_CaDET.Model.PlatformConnection
{
    interface IPlatformConnection
    {
        public Task<ClassQualityAnalysisResponse> GetClassQualityAnalysisAsync(string sourceCode);
    }
}