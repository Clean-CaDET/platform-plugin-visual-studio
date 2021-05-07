using Clean_CaDET.Model.PlatformConnection.DTOs.SubmissionEvaluation;
using System.Threading.Tasks;

namespace Clean_CaDET.Model.PlatformConnection
{
    internal interface IPlatformConnection
    {
        public Task<ChallengeEvaluationDTO> SubmitChallengeAsync(string[] sourceCode, int challengeId, int learnerId);
    }
}