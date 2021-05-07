﻿using Clean_CaDET.Model.PlatformConnection;
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
        public PlatformService()
        {
            _explorer = new SolutionExplorer();
            _platformConnection = new CaDETConnection();
        }

        public async Task<ChallengeEvaluationDTO> SubmitChallengeAsync(string codePath)
        {
            var sourceCode = await _explorer.CollectSourceCodeAsync(codePath);
            return await _platformConnection.SubmitChallengeAsync(sourceCode, 0, 0);
        }
    }
}
