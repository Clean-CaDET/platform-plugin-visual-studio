using Clean_CaDET.Model.PlatformConnection;
using Clean_CaDET.Model.PlatformConnection.DTOs;
using Clean_CaDET.Model.SolutionParser;
using System.Threading.Tasks;

namespace Clean_CaDET.Model
{
    public sealed class PlatformService
    {
        private readonly SolutionExplorer _explorer;
        private readonly IPlatformConnection _platformConnection;
        //TODO: DI
        public PlatformService()
        {
            _explorer = new SolutionExplorer();
            _platformConnection = new CaDETConnection();
        }

        public async Task<ClassQualityAnalysisResponse> AnalyzeClassQualityAsync(string classPath)
        {
            string sourceCode = await _explorer.FindClassCodeAsync(classPath);
            return await _platformConnection.GetClassQualityAnalysisAsync(sourceCode);
        }
    }
}
