using Clean_CaDET.Model.PlatformConnection;
using Clean_CaDET.Model.PlatformConnection.DTOs.QualityAnalysis;
using Clean_CaDET.Model.PlatformConnection.DTOs.SubmissionEvaluation;
using Clean_CaDET.Model.SolutionParser;
using System.Threading.Tasks;

namespace Clean_CaDET.Model
{
    public sealed class PlatformService
    {
        private readonly SolutionExplorer _explorer;
        private readonly IPlatformConnection _platformConnection;
        //TODO: DI
        public PlatformService(string url)
        {
            _explorer = new SolutionExplorer();
            _platformConnection = new CaDETConnection(url);
            //_platformConnection = new MockConnection();
        }

        public async Task<ChallengeEvaluationDTO> SubmitChallengeAsync(string codePath, int challengeId, int learnerId)
        {
            var sourceCode = await _explorer.CollectSourceCodeAsync(codePath);
            //TODO: Read challenge and learner id from UI/plugin
            return await _platformConnection.SubmitChallengeAsync(sourceCode, challengeId, learnerId);
        }

        internal async Task<CodeEvaluationDTO> AnalyzeCodeAsync(string codePath)
        {
            var sourceCode = await _explorer.CollectSourceCodeAsync(codePath);
            return await _platformConnection.EvaluateCodeQualityAsync(sourceCode);
        }
    }
}
