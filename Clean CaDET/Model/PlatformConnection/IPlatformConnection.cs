using System.Threading.Tasks;
using Clean_CaDET.Model.PlatformConnection.DTOs;

namespace Clean_CaDET.Model.PlatformConnection
{
    interface IPlatformConnection
    {
        public Task<ChallengeEvaluationDTO> SubmitChallengeAsync(string[] sourceCode, int challengeId, int learnerId);
    }
}